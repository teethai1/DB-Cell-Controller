Imports System.Data.SqlClient

Public Class WorkingSlipQRCode
    Public Sub SplitQRCode(ByVal QRCode As String)
        'VSON008X20BR24T08NUX-WG(8G9)  1319A6224VC008-M249       0110XA36      2AM8U-2897  TR  V96       T08                 2 R4T08NUXOA  B3054           VSON008X20          BR24T08NUX-WG       V96         04000         BR24T08NUX-WGTR     BR24T08NUX-WGTR     
        If QRCode Is Nothing Then
            _Pass = False
            Exit Sub
        End If

        If QRCode.Length = 252 Or QRCode.Length = My.Settings.WorkingSlipQRLenght Then
            _QRCode = QRCode
            _Package = QRCode.Substring(0, 10).Trim
            _Device = QRCode.Substring(10, 20).Trim
            _LotNo = QRCode.Substring(30, 10).Trim
            _FrameType = QRCode.Substring(40, 16).Trim
            _FarSetDirection = QRCode.Substring(56, 4).Trim
            _Code = QRCode.Substring(60, 10).Trim
            _WFLotNo = QRCode.Substring(70, 12).Trim
            _TapingDirection = QRCode.Substring(82, 3).Trim
            _Stamp_Laser = QRCode.Substring(85, 1).Trim
            _Marking_Spec1 = QRCode.Substring(86, 10).Trim
            _Marking_Spec2 = QRCode.Substring(96, 10).Trim
            _Marking_Spec3 = QRCode.Substring(106, 10).Trim
            _MarkingStep = QRCode.Substring(116, 1).Trim
            _OS_FTChange = QRCode.Substring(117, 1).Trim
            _OSProgram = QRCode.Substring(118, 12).Trim
            _MoldType = QRCode.Substring(130, 16).Trim
            _NewPackageName = QRCode.Substring(146, 20).Trim
            _FTDevice = QRCode.Substring(166, 20).Trim
            _M_No = QRCode.Substring(186, 10).Trim
            _PB_Free = QRCode.Substring(196, 1).Trim
            _UL_Mark = QRCode.Substring(197, 1).Trim
            _ReelCount = QRCode.Substring(198, 5).Trim
            _ClaimCountermeasure = QRCode.Substring(203, 4).Trim
            _SubRank = QRCode.Substring(207, 3).Trim
            _Mask = QRCode.Substring(210, 2).Trim
            _DeviceTPDirection1 = QRCode.Substring(212, 20).Trim
            _DeviceTPDirection2 = QRCode.Substring(232, 20).Trim

            _Pass = True
        Else
            _Pass = False
        End If
    End Sub

    Public Function TransactionDataSave(ByVal QRcode252 As String) As String

        Dim SplitQR As New WorkingSlipQRCode
        SplitQR.SplitQRCode(QRcode252)
        If SplitQR.Pass = False Then
            Return "False : SplitQR Check Error"
        End If
        Dim ret As Boolean = False
        Dim objCom As New SqlCommand()
        'Dim myConnectionString As String = "Data Source=172.16.0.102;Initial Catalog=DBx;Persist Security Info=True;User ID=DBxUser;Password="

        Dim myConnectionString As String = My.Settings.SecsConnStr
        Using con As SqlConnection = New SqlConnection

            con.ConnectionString = myConnectionString
            con.Open()
            Try
                Using cmd As SqlCommand = New SqlCommand
                    cmd.Connection = con
                    Dim CmdText As String = "INSERT INTO [DBx].[dbo].[TransactionData] ([LotNo],[Package],[Device],[FrameNo],[FASetDirection],[CodeNo],[WaferLotNo],[TapingDirection],[MarkType],[MarkTextLine3],[MarkTextLine2],[MarkTextLine1],[NumberOfStampStep],[OSFT],[OSProgram],[MoldType],[NewFormName],[FTForm],[MarkNo],[PDFree],[ULMark],[ReelCount],[CleamCounterMeasure],[SubRank],[Mask],[ETC1],[ETC2]) VALUES (@LotNo,@Package,@Device,@FrameNo,@FASetDirection,@CodeNo,@WaferLotNo,@TapingDirection,@MarkType,@MarkTextLine3,@MarkTextLine2,@MarkTextLine1,@NumberOfStampStep,@OSFT,@OSProgram,@MoldType,@NewFormName,@FTForm,@MarkNo,@PDFree,@ULMark,@ReelCount,@CleamCounterMeasure,@SubRank,@Mask,@ETC1,@ETC2)"
                    cmd.CommandText = CmdText
                    cmd.Parameters.Add("@LotNo", SqlDbType.VarChar, 10).Value = SplitQR.LotNo
                    cmd.Parameters.Add("@Package", SqlDbType.VarChar, 10).Value = SplitQR.Package
                    cmd.Parameters.Add("@Device", SqlDbType.VarChar, 20).Value = SplitQR.Device
                    cmd.Parameters.Add("@FrameNo", SqlDbType.VarChar, 16).Value = SplitQR.FrameType
                    cmd.Parameters.Add("@FASetDirection", SqlDbType.VarChar, 4).Value = SplitQR.FarSetDirection
                    cmd.Parameters.Add("@CodeNo", SqlDbType.VarChar, 10).Value = SplitQR.Code
                    cmd.Parameters.Add("@WaferLotNo", SqlDbType.VarChar, 12).Value = SplitQR.WFLotNo
                    cmd.Parameters.Add("@TapingDirection", SqlDbType.VarChar, 3).Value = SplitQR.Tapingdirection
                    cmd.Parameters.Add("@MarkType", SqlDbType.VarChar, 1).Value = SplitQR.Stamp_Laser
                    cmd.Parameters.Add("@MarkTextLine3", SqlDbType.VarChar, 10).Value = SplitQR.Marking_Spec3
                    cmd.Parameters.Add("@MarkTextLine2", SqlDbType.VarChar, 10).Value = SplitQR.Marking_Spec2
                    cmd.Parameters.Add("@MarkTextLine1", SqlDbType.VarChar, 10).Value = SplitQR.Marking_Spec1
                    cmd.Parameters.Add("@NumberOfStampStep", SqlDbType.VarChar, 1).Value = SplitQR.MarkingStep
                    cmd.Parameters.Add("@OSProgram", SqlDbType.VarChar, 12).Value = SplitQR.OSProgram
                    cmd.Parameters.Add("@OSFT", SqlDbType.VarChar, 1).Value = SplitQR.OS_FTChange
                    cmd.Parameters.Add("@MoldType", SqlDbType.VarChar, 16).Value = SplitQR.MoldType
                    cmd.Parameters.Add("@NewFormName", SqlDbType.VarChar, 20).Value = SplitQR.NewPackageName
                    cmd.Parameters.Add("@FTForm", SqlDbType.VarChar, 20).Value = SplitQR.FTDevice
                    cmd.Parameters.Add("@MarkNo", SqlDbType.VarChar, 10).Value = SplitQR.M_No
                    cmd.Parameters.Add("@PDFree", SqlDbType.VarChar, 1).Value = SplitQR.PB_Free
                    cmd.Parameters.Add("@ULMark", SqlDbType.VarChar, 1).Value = SplitQR.UL_Mark
                    cmd.Parameters.Add("@ReelCount", SqlDbType.VarChar, 5).Value = SplitQR.ReelCount
                    cmd.Parameters.Add("@CleamCounterMeasure", SqlDbType.VarChar, 4).Value = SplitQR.ClaimCountermeasure
                    cmd.Parameters.Add("@SubRank", SqlDbType.VarChar, 3).Value = SplitQR.SubRank
                    cmd.Parameters.Add("@Mask", SqlDbType.VarChar, 2).Value = SplitQR.Mask
                    cmd.Parameters.Add("@ETC1", SqlDbType.VarChar, 20).Value = SplitQR.DeviceTPDirection1
                    cmd.Parameters.Add("@ETC2", SqlDbType.VarChar, 20).Value = SplitQR.DeviceTPDirection2
                    Try
                        cmd.ExecuteNonQuery()
                        Return "True :"
                    Catch ex As SqlException
                        If ex.Number = 2627 Then
                            Return "False : A record already exists with the same primary key"
                        Else
                            Return "False : Could not add record, please ensure the fields are correctly filled out"
                        End If

                    End Try

                End Using

            Catch ex As Exception
                Return "False :"
            End Try
            con.Close()
        End Using



    End Function

    Public _QRCode As String
    Property QRCode() As String
        Get
            Return _QRCode
        End Get
        Set(ByVal value As String)
            _QRCode = value
        End Set
    End Property



    Public _Pass As Boolean
    Property Pass() As Boolean
        Get
            Return _Pass
        End Get
        Set(ByVal value As Boolean)
            _Pass = value
        End Set
    End Property


    Public _Package As String
    Property Package() As String
        Get
            Return _Package
        End Get
        Set(ByVal value As String)
            _Package = value
        End Set
    End Property

    Public _Device As String
    Property Device() As String
        Get
            Return _Device
        End Get
        Set(ByVal value As String)
            _Device = value
        End Set
    End Property

    Public _LotNo As String
    Property LotNo() As String
        Get
            Return _LotNo
        End Get
        Set(ByVal value As String)
            _LotNo = value
        End Set
    End Property

    Public _FrameType As String
    Property FrameType() As String
        Get
            Return _FrameType
        End Get
        Set(ByVal value As String)
            _FrameType = value
        End Set
    End Property

    Public _FarSetDirection As String
    Property FarSetDirection() As String
        Get
            Return _FarSetDirection
        End Get
        Set(ByVal value As String)
            _FarSetDirection = value
        End Set
    End Property

    Public _Code As String
    Property Code() As String
        Get
            Return _Code
        End Get
        Set(ByVal value As String)
            _Code = value
        End Set
    End Property

    Public _WFLotNo As String
    Property WFLotNo() As String
        Get
            Return _WFLotNo
        End Get
        Set(ByVal value As String)
            _WFLotNo = value
        End Set
    End Property

    Public _TapingDirection As String
    Property Tapingdirection() As String
        Get
            Return _TapingDirection
        End Get
        Set(ByVal value As String)
            _TapingDirection = value
        End Set
    End Property

    Public _Stamp_Laser As String
    Property Stamp_Laser() As String
        Get
            Return _Stamp_Laser
        End Get
        Set(ByVal value As String)
            _Stamp_Laser = value
        End Set
    End Property

    Public _Marking_Spec1 As String
    Property Marking_Spec1() As String
        Get
            Return _Marking_Spec1
        End Get
        Set(ByVal value As String)
            _Marking_Spec1 = value
        End Set
    End Property

    Public _Marking_Spec2 As String
    Property Marking_Spec2() As String
        Get
            Return _Marking_Spec2
        End Get
        Set(ByVal value As String)
            _Marking_Spec2 = value
        End Set
    End Property

    Public _Marking_Spec3 As String
    Property Marking_Spec3() As String
        Get
            Return _Marking_Spec3
        End Get
        Set(ByVal value As String)
            _Marking_Spec3 = value
        End Set
    End Property

    Public _MarkingStep As String
    Property MarkingStep() As String
        Get
            Return _MarkingStep
        End Get
        Set(ByVal value As String)
            _MarkingStep = value
        End Set
    End Property

    Public _OS_FTChange As String
    Property OS_FTChange() As String
        Get
            Return _OS_FTChange
        End Get
        Set(ByVal value As String)
            _OS_FTChange = value
        End Set
    End Property

    Public _OSProgram As String
    Property OSProgram() As String
        Get
            Return _OSProgram
        End Get
        Set(ByVal value As String)
            _OSProgram = value
        End Set
    End Property

    Public _MoldType As String
    Property MoldType() As String
        Get
            Return _MoldType
        End Get
        Set(ByVal value As String)
            _MoldType = value
        End Set
    End Property

    Public _NewPackageName As String
    Property NewPackageName() As String
        Get
            Return _NewPackageName
        End Get
        Set(ByVal value As String)
            _NewPackageName = value
        End Set
    End Property

    Public _FTDevice As String
    Property FTDevice() As String
        Get
            Return _FTDevice
        End Get
        Set(ByVal value As String)
            _FTDevice = value
        End Set
    End Property

    Public _M_No As String
    Property M_No() As String
        Get
            Return _M_No
        End Get
        Set(ByVal value As String)
            _M_No = value
        End Set
    End Property

    Public _PB_Free As String
    Property PB_Free() As String
        Get
            Return _PB_Free
        End Get
        Set(ByVal value As String)
            _PB_Free = value
        End Set
    End Property

    Public _UL_Mark As String
    Property UL_Mark() As String
        Get
            Return _UL_Mark
        End Get
        Set(ByVal value As String)
            _UL_Mark = value
        End Set
    End Property

    Public _ReelCount As String
    Property ReelCount() As String
        Get
            Return _ReelCount
        End Get
        Set(ByVal value As String)
            _ReelCount = value
        End Set
    End Property

    Public _ClaimCountermeasure As String
    Property ClaimCountermeasure() As String
        Get
            Return _ClaimCountermeasure
        End Get
        Set(ByVal value As String)
            _ClaimCountermeasure = value
        End Set
    End Property

    Public _SubRank As String
    Property SubRank() As String
        Get
            Return _SubRank
        End Get
        Set(ByVal value As String)
            _SubRank = value
        End Set
    End Property

    Public _Mask As String
    Property Mask() As String
        Get
            Return _Mask
        End Get
        Set(ByVal value As String)
            _Mask = value
        End Set
    End Property

    Public _DeviceTPDirection1 As String
    Property DeviceTPDirection1() As String
        Get
            Return _DeviceTPDirection1
        End Get
        Set(ByVal value As String)
            _DeviceTPDirection1 = value
        End Set
    End Property

    Public _DeviceTPDirection2 As String
    Property DeviceTPDirection2() As String
        Get
            Return _DeviceTPDirection2
        End Get
        Set(ByVal value As String)
            _DeviceTPDirection2 = value
        End Set
    End Property
End Class
