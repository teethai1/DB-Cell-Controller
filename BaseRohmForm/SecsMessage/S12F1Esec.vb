Imports XtraLibrary.SecsGem

Public Class S12F1Esec
    Inherits SecsMessageBase

    Private m_L15 As New SecsItemList("L15")

    Private m_MID As New SecsItemAscii("MID")
    Private m_IDTYP As New SecsItemBinary("IDTYP")
    Private m_FNLOC As New SecsItemU2("FNLOC")
    Private m_FFROT As New SecsItemU2("FFROT")
    Private m_ORLOC As New SecsItemBinary("ORLOC")
    Private m_RPSEL As New SecsItemU1("RPSEL")

    Private m_Ln As New SecsItemList("LnREFP")
    'Private m_Reep As New SecsItemI2("Reep")
    'Private m_Reep1 As New SecsItemI2("Reep1")

    Private m_DUTMS As New SecsItemAscii("DUTMS")
    Private m_XDIES As New SecsItemU2("XDIES")
    Private m_YDIES As New SecsItemU2("YDIES")
    Private m_ROWCT As New SecsItemU2("ROWCT")
    Private m_COLCT As New SecsItemU2("COLCT")
    Private m_NULBC As New SecsItemAscii("NULBC")
    Private m_PRDCT As New SecsItemU2("PRDCT")
    Private m_PRAXI As New SecsItemBinary("PRAXI")

    Public Sub New()
        MyBase.New(12, 1, True)
        Me.AddItem(m_L15)
        m_L15.AddItem(m_MID)
        m_L15.AddItem(m_IDTYP)
        m_L15.AddItem(m_FNLOC)
        m_L15.AddItem(m_FFROT)
        m_L15.AddItem(m_ORLOC)
        m_L15.AddItem(m_RPSEL)

        m_L15.AddItem(m_Ln)
        'm_Ln.AddItem(m_Reep)
        'm_Ln.AddItem(m_Reep1)
        m_L15.AddItem(m_DUTMS)
        m_L15.AddItem(m_XDIES)
        m_L15.AddItem(m_YDIES)
        m_L15.AddItem(m_ROWCT)
        m_L15.AddItem(m_COLCT)
        m_L15.AddItem(m_NULBC)
        m_L15.AddItem(m_PRDCT)
        m_L15.AddItem(m_PRAXI)
    End Sub




    Public ReadOnly Property Reep() As List(Of Point)
        Get
            Dim REEPx As New List(Of Point)
            For Each item As SecsItem In m_Ln.Value

                If item.Value.GetType.Name = "Int64[]" Then  'I8
                    Dim a = CType(item.Value, Int64())
                    Dim P As New Point(CInt(a(0)), CInt(a(1)))
                    REEPx.Add(P)

                ElseIf item.Value.GetType.Name = "Int32[]" Then  'I4
                    Dim a = CType(item.Value, Int32())
                    Dim P As New Point(CInt(a(0)), CInt(a(1)))
                    REEPx.Add(P)

                ElseIf item.Value.GetType.Name = "Int16[]" Then  'I2
                    Dim a = CType(item.Value, Int16())
                    Dim P As New Point(CInt(a(0)), CInt(a(1)))
                    REEPx.Add(P)

                ElseIf item.Value.GetType.Name = "SByte[]" Then 'I1
                    Dim a = CType(item.Value, SByte())
                    Dim P As New Point(CInt(a(0)), CInt(a(1)))
                    REEPx.Add(P)

                End If
            Next
            Return REEPx


        End Get
    End Property

    Public ReadOnly Property MID() As String
        Get
            Return m_MID.Value
        End Get
    End Property

    Public ReadOnly Property IDTYP() As Byte
        Get
            If m_IDTYP.Value.Length <> 0 Then
                Return m_IDTYP.Value(0)
            End If
        End Get
    End Property

    Public ReadOnly Property FNLOC() As UShort
        Get
            Return m_FNLOC.Value(0)
        End Get
    End Property

    Public ReadOnly Property FFROT() As UShort
        Get
            If m_FFROT.Value.Length <> 0 Then
                Return m_FFROT.Value(0)
            End If
        End Get
    End Property

    Public ReadOnly Property ORLOC() As Byte
        Get
            If m_ORLOC.Value.Length <> 0 Then
                Return m_ORLOC.Value(0)
            End If
        End Get
    End Property

    Public ReadOnly Property RPSEL() As Byte
        Get
            If m_RPSEL.Value.Length <> 0 Then
                Return m_RPSEL.Value(0)
            End If
        End Get
    End Property

    Public ReadOnly Property DUTMS() As String
        Get
            If m_DUTMS.Value.Length <> 0 Then
                Return m_DUTMS.Value(0)
            End If

        End Get
    End Property

    Public ReadOnly Property XDIES() As UShort
        Get
            If m_XDIES.Value.Length <> 0 Then
                Return m_XDIES.Value(0)
            End If
        End Get
    End Property

    Public ReadOnly Property YDIES() As UShort
        Get
            If m_YDIES.Value.Length <> 0 Then
                Return m_YDIES.Value(0)
            End If
        End Get
    End Property

    Public ReadOnly Property ROWCT() As UShort
        Get
            If m_ROWCT.Value.Length <> 0 Then
                Return m_ROWCT.Value(0)
            End If
        End Get
    End Property

    Public ReadOnly Property COLCT() As UShort
        Get
            If m_COLCT.Value.Length <> 0 Then
                Return m_COLCT.Value(0)
            End If
        End Get
    End Property

    Public ReadOnly Property NULBC() As String
        Get
            If m_NULBC.Value.Length <> 0 Then
                Return m_NULBC.Value(0)
            End If
        End Get
    End Property

    Public ReadOnly Property PRDCT() As UShort
        Get
            If m_PRDCT.Value.Length <> 0 Then
                Return CUShort(m_PRDCT.Value(0))
            End If
        End Get
    End Property

    Public ReadOnly Property PRAXI() As Byte
        Get
            If m_PRAXI.Value.Length <> 0 Then
                Return m_PRAXI.Value(0)
            End If
        End Get
    End Property

End Class
