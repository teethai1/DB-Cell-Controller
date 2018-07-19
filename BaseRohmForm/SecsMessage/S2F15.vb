Imports XtraLibrary.SecsGem
Public Class S2F15
    Inherits SecsMessageBase

    Private m_Ln As SecsItemList

    Public Sub New()
        MyBase.New(2, 15, True)
        m_Ln = New SecsItemList("Ln")
        AddItem(m_Ln)
    End Sub

    Public Sub AddListEcid(ByVal ecid As UInteger, ByVal ecv As String, ByVal Format As SecsFormat) '160823 \783 Addition SecsFormat
        Dim Type As New SecsDataType
        Dim l2 As SecsItemList = New SecsItemList("L2")
        Try

      
            If Type.ECID = ECIDSecsForm.U2 Then    '161222 \783 Config SECSGEM
                Dim ecidItem As SecsItemU2 = New SecsItemU2("ECID" & m_Ln.Value.Count.ToString(), CUShort(ecid))
                l2.AddItem(ecidItem)
            Else
                Dim ecidItem As SecsItemU4 = New SecsItemU4("ECID" & m_Ln.Value.Count.ToString(), ecid)
                l2.AddItem(ecidItem)
            End If


        If Format = SecsFormat.U2 Then    '160803 \783 Type Common
            Dim ecvItem As SecsItemU2 = New SecsItemU2("ECV" & m_Ln.Value.Count.ToString(), CUShort(ecv))
            l2.AddItem(ecvItem)
        ElseIf Format = SecsFormat.U4 Then
            Dim ecvItem As SecsItemU4 = New SecsItemU4("ECV" & m_Ln.Value.Count.ToString(), CUInt(ecv))
            l2.AddItem(ecvItem)
        ElseIf Format = SecsFormat.U8 Then
            Dim ecvItem As SecsItemU8 = New SecsItemU8("ECV" & m_Ln.Value.Count.ToString(), CULng(ecv))
            l2.AddItem(ecvItem)
        ElseIf Format = SecsFormat.U1 Then
            Dim ecvItem As SecsItemU1 = New SecsItemU1("ECV" & m_Ln.Value.Count.ToString(), CByte(ecv))
            l2.AddItem(ecvItem)
        ElseIf Format = SecsFormat.Binary Then
            Dim ecvItem As SecsItemBinary = New SecsItemBinary("ECV" & m_Ln.Value.Count.ToString(), CByte(ecv))
            l2.AddItem(ecvItem)
        ElseIf Format = SecsFormat.Bool Then
            Dim ecvItem As SecsItemBoolean = New SecsItemBoolean("ECV" & m_Ln.Value.Count.ToString(), CBool(ecv))
            l2.AddItem(ecvItem)

        ElseIf Format = SecsFormat.I1 Then
            Dim ecvItem As SecsItemI1 = New SecsItemI1("ECV" & m_Ln.Value.Count.ToString(), CSByte(ecv))
            l2.AddItem(ecvItem)


        ElseIf Format = SecsFormat.I2 Then
            Dim ecvItem As SecsItemI2 = New SecsItemI2("ECV" & m_Ln.Value.Count.ToString(), CShort(ecv))
            l2.AddItem(ecvItem)

        ElseIf Format = SecsFormat.I4 Then
            Dim ecvItem As SecsItemI4 = New SecsItemI4("ECV" & m_Ln.Value.Count.ToString(), CInt(ecv))
            l2.AddItem(ecvItem)
        ElseIf Format = SecsFormat.I8 Then
            Dim ecvItem As SecsItemI8 = New SecsItemI8("ECV" & m_Ln.Value.Count.ToString(), CLng(ecv))
            l2.AddItem(ecvItem)
        ElseIf Format = SecsFormat.F4 Then
            Dim ecvItem As SecsItemF4 = New SecsItemF4("ECV" & m_Ln.Value.Count.ToString(), CSng(ecv))
            l2.AddItem(ecvItem)
        ElseIf Format = SecsFormat.F8 Then
            Dim ecvItem As SecsItemF8 = New SecsItemF8("ECV" & m_Ln.Value.Count.ToString(), CDbl(ecv))
            l2.AddItem(ecvItem)
        Else
            Dim ecvItem As SecsItemAscii = New SecsItemAscii("ECV" & m_Ln.Value.Count.ToString(), ecv)
            l2.AddItem(ecvItem)
        End If

        m_Ln.AddItem(l2)
        Catch ex As Exception
            SaveCatchLog(ex.ToString, "S2F15_AddListEcid()")
        End Try
    End Sub
   
End Class
