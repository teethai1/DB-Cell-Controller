Imports XtraLibrary.SecsGem
Public Class S12F4
    Inherits SecsMessageBase
    Private m_L15 As New SecsItemList("L15")

    Public Sub New()
        MyBase.New(12, 4, False)
        Me.AddItem(m_L15)
    End Sub

    Public Sub SetS12F4_Esec(ByVal MID As String, ByVal IDTYP As Byte, ByVal FNLOC As UShort, ByVal ORLOC As Byte, ByVal REFP As List(Of Point), ByVal ROWCT As UShort, ByVal COLCT As UShort, ByVal PRDCT As UInteger, ByVal BCEQU As String, ByVal NULBC As String)

        Dim m_MID As New SecsItemAscii("MID", MID)
        Dim m_IDTYP As New SecsItemBinary("IDTYP", IDTYP)
        Dim m_FNLOC As New SecsItemU2("FNLOC", FNLOC)
        Dim m_ORLOC As New SecsItemBinary("ORLOC", ORLOC)
        Dim m_RPSEL As New SecsItemU1("RPSEL", CByte(REFP.Count))
        Dim m_Ln As New SecsItemList("LnREFP")

        Dim m_DUTMS As New SecsItemAscii("DUTMS", CStr(0))
        Dim m_XDIES As New SecsItemU2("XDIES", 0)
        Dim m_YDIES As New SecsItemU2("YDIES", 0)
        Dim m_ROWCT As New SecsItemU2("ROWCT", ROWCT)
        Dim m_COLCT As New SecsItemU2("COLCT", COLCT)
        Dim m_PRDCT As New SecsItemU4("PRDCT", PRDCT)
        Dim m_BCEQU As New SecsItemAscii("BCEQU", BCEQU)
        Dim m_NULBC As New SecsItemAscii("NULBC", NULBC)
        Dim m_MLCL As New SecsItemU4("MLCL", 0)

        For i = 0 To REFP.Count - 1                   'if More 1 Ref   Refp 
            m_Ln.AddItem(New SecsItemI2("Reep", {CShort(REFP(i).X), CShort(REFP(i).Y)}))
        Next

        m_L15.AddItem(m_MID)
        m_L15.AddItem(m_IDTYP)
        m_L15.AddItem(m_FNLOC)
        m_L15.AddItem(m_ORLOC)
        m_L15.AddItem(m_RPSEL)
        m_L15.AddItem(m_Ln)
        m_L15.AddItem(m_DUTMS)
        m_L15.AddItem(m_XDIES)
        m_L15.AddItem(m_YDIES)
        m_L15.AddItem(m_ROWCT)
        m_L15.AddItem(m_COLCT)
        m_L15.AddItem(m_PRDCT)
        m_L15.AddItem(m_BCEQU)
        m_L15.AddItem(m_NULBC)
        m_L15.AddItem(m_MLCL)

    End Sub

End Class