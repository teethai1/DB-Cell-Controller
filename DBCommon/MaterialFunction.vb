Public Class MaterialFunction

    Public Function PreformCheck(ByVal QR_Preform As String) As MatClass
        Dim mat As New MatClass
        Try
            '=============================== เชคตัวอักษร format ==========================
            Dim revData As String = QR_Preform.Replace(".", "")
            Dim SplitData As String() = revData.Split(CChar(","))
            If UBound(SplitData) <> 1 Then 'NG
                mat.AlarmMessege = ("รูปแบบข้อมุล Preform Material QR ไม่ถูกต้อง ค่าที่อ่านได้คือ(" & QR_Preform & ")") ', "Preform Material QR Input", "รูปแบบข้อมุล ไม่ใช (MAT,ตัวเลข PreForm ID)")
                mat.IsPass = False
                Return mat
            Else
                If SplitData(0).Contains("MAT") = False OrElse IsNumeric(SplitData(1)) = False Then
                    mat.AlarmMessege = ("รูปแบบข้อมุล Preform Material QR ไม่ถูกต้อง ค่าที่อ่านได้คือ(" & QR_Preform & ")") ', "Preform Material QR Input", "รูปแบบข้อมุล ไม่ใช (MAT,ตัวเลข PreForm ID)")
                    mat.IsPass = False
                    Return mat
                End If
            End If
            '=========================== เชค Preform Status "New Out" เท่านั้น ========================
            Dim QRPreform As String = SplitData(1)
            Dim PreformStatusAdpater As New DBxDataSetTableAdapters.QueriesTableAdapter1
            Dim PreformState As String = PreformStatusAdpater.PreformStatus(CInt(QRPreform))
            If PreformState Is Nothing Then
                mat.AlarmMessege = ("ข้อมูล Preform (" & QRPreform & ") ไม่มีในระบบ Material กรุณานำไปเข้าระบบที่เครื่อง Material ก่อน นำมาใช้งาน") ', "Preform Material QR Input", "ไม่พบค่านี้ใน MAT.Transaction Table")
                mat.IsPass = False
                Return mat
            ElseIf PreformState = "New In" Then
                mat.AlarmMessege = ("Preform สถานะเป็น New In กรุณาเปลี่ยนสถานะที่เครื่อง Material เป็นสถานะ New Out เพื่อใช้งาน") ', "Preform Material QR Input", "TransactionType เป็น New In ไม่ได้")
                mat.IsPass = False
                Return mat
            ElseIf PreformState = "New Out" Then
                GoTo PreformStateOK
            ElseIf PreformState = "Terminate" Then
                mat.AlarmMessege = ("Preform(" & QRPreform & ") นี้หมดอายุแล้ว กรุณาเปลี่ยน Preform") ', "Preform Material QR Input", "TransactionType เป็น Terminate ไม่ได้")
                mat.IsPass = False
                Return mat
            Else
                mat.AlarmMessege = ("ข้อมูล Preform : MAT," & QRPreform & " สถานะ " & PreformState & " ไม่สามารถใช้งานได้") ', "Preform Material QR Input", "ตรวจสอบ TransactionType สถานะอื่น")
                mat.IsPass = False
                Return mat
            End If
            '=========================================================

PreformStateOK:
            Dim PreformDetailAdapter As New DBxDataSetTableAdapters.ExpirePreformTableAdapter
            Dim PreformDetail As DBxDataSet.ExpirePreformDataTable = PreformDetailAdapter.GetData(CInt(QRPreform))

            Dim PreformLotNo As String = ""
            Dim PreformType As String = ""
            Dim PreformInput As New Date
            Dim PreformExp As New Date
            If PreformDetail.Rows.Count <> 0 Then
                PreformLotNo = PreformDetail.Item(0).MakerLotNo
                PreformType = PreformDetail.Item(0).MaterialModel
                PreformInput = PreformDetail.Item(0).TransactionDate
                PreformExp = PreformDetail.Item(0).ExpireTime
            Else
                mat.AlarmMessege = ("ไม่มีข้อมูลในระบบ Preform(" & QRPreform & ") กรุณาติดต่อ System") ', "Preform Material QR Input", "ExpirePreformTableAdapter Query Rows = 0")
                mat.IsPass = False
                Return mat
            End If

            'เชควันหมดอายุ()
            Dim strexp As String = PreformCondition(PreformExp)
            Select Case strexp
                Case PreformStatus.Normal.ToString
                Case PreformStatus.Lower4Hour.ToString
                    mat.AlarmMessege = ("อายุของ Preform ต่ากว่า 4 ชั่วโมง กรุณาเปลี่ยนหลอดด้วยครับ") ', "Preform life time", "LifeTime < 240 Min")
                    mat.IsPass = False
                    Return mat
                Case PreformStatus.Expired.ToString
                    mat.AlarmMessege = ("Preform นี้หมดอายุแล้ว") ', "Preform life time", "LifeTime Over")
                    mat.IsPass = False
                    Return mat
            End Select

            mat.PreformExp = PreformExp
            mat.PreformInput = PreformInput
            mat.PreformLotNo = PreformLotNo
            mat.PreformType = PreformType
            mat.MatQrCode = QRPreform
            mat.IsPass = True
            Return mat

        Catch ex As Exception
            mat.IsPass = False
            mat.AlarmMessege = ("Catch Error ตรวจสอบดู CatchLog") ', "PreformCheck()", "TryCatchEnd", "")
            Return mat
        End Try

    End Function

    Private Function PreformCondition(ByVal exp As Date) As String
        Dim ret As String = ""
        Dim MinTotal As Long
        MinTotal = DateDiff(DateInterval.Minute, Now, exp)

        If MinTotal > 240 Then
            ret = PreformStatus.Normal.ToString
        ElseIf MinTotal < 240 AndAlso MinTotal > 0 Then 'ใกล้หมดไม่ให้รันต่อ 0-4 HR ส้มเข้ม
            ret = PreformStatus.Lower4Hour.ToString
        Else 'แดง
            ret = PreformStatus.Expired.ToString ' หมดอายุ
        End If

        Return ret
    End Function
    Public Function FrameCheck(QrFrame As String) As MatClass
        Dim mat As New MatClass

        Dim FrameMat As New DBxDataSet.IS_MATL_STOCK_FILEDataTable
        Dim FrameMatAdapter As New DBxDataSetTableAdapters.IS_MATL_STOCK_FILETableAdapter
        FrameMatAdapter.FrameMaterail_IS(FrameMat, QrFrame)

        If FrameMat.Rows.Count = 0 Then
            mat.IsPass = False
            mat.AlarmMessege = "QR Frame(" & QrFrame & ") นี้ยังไม่ถูกบันทึกลงในฐานข้อมูลของ Frame Material ติดต่อห้อง Frame Material หรือ Manual Input"
            Return mat
        End If


        mat.FrameLotNo = FrameMat(0).FRAME_LOT1.Trim
        mat.FrameType = FrameMat(0).PROD_NAME.Trim
        mat.MatQrCode = QrFrame
        mat.IsPass = True
        Return mat

    End Function

    Enum PreformStatus
        Blue40
        Sky32_40
        Green24_32
        Yello10_24
        Orange4_10
        DarkOrange0_4

        Normal
        Expired
        Lower4Hour
    End Enum

End Class
