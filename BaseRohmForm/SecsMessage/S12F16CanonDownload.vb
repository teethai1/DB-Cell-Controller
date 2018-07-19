Imports XtraLibrary.SecsGem

Public Class S12F16CanonDownload
    Inherits SecsMessageBase
    Private m_L04 As New SecsItemList("L4")
    Public Sub New()
        MyBase.New(12, 16, False)
        AddItem(m_L04)

    End Sub

    Public Sub SetS12F16(ByVal MID As String, ByVal IDTYP As Byte, ByVal WaferMap As List(Of String))

        Dim strWaferMap As String = ""
        For Each strData As String In WaferMap
            strWaferMap &= strData.Replace(",", "")
        Next

        Dim m_MID As New SecsItemAscii("MID", MID)
        Dim m_IDTYP As New SecsItemBinary("IDTYP", IDTYP)
        Dim m_STRP As New SecsItemI4("Reep", {0, 0})
        Dim m_BINLT As New SecsItemAscii("BINLT", strWaferMap)

        m_L04.AddItem(m_MID)
        m_L04.AddItem(m_IDTYP)
        m_L04.AddItem(m_STRP)
        m_L04.AddItem(m_BINLT)

    End Sub

    Public Sub SetS12F16(ByVal MID As String, ByVal IDTYP As Byte, ByVal WaferMap As String)

        Dim strWaferMap As String = WaferMap


        Dim m_MID As New SecsItemAscii("MID", MID)
        Dim m_IDTYP As New SecsItemBinary("IDTYP", IDTYP)
        Dim m_STRP As New SecsItemI4("Reep", {0, 0})
        Dim m_BINLT As New SecsItemAscii("BINLT", strWaferMap)

        m_L04.AddItem(m_MID)
        m_L04.AddItem(m_IDTYP)
        m_L04.AddItem(m_STRP)
        m_L04.AddItem(m_BINLT)

    End Sub


End Class
