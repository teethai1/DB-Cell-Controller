Imports XtraLibrary.SecsGem

Public Class S2F18
    Inherits SecsMessageBase

    Private m_TimeItem As SecsItemAscii
    Private m_Time As DateTime
    Private m_Format As TimeFormat

    Public Sub New()
        Me.New(TimeFormat.A12)
    End Sub

    Public Sub New(ByVal timeFormat As TimeFormat)
        MyBase.New(2, 18, False)
        'create message item instance
        m_TimeItem = New SecsItemAscii("TIME")

        'custom value
        m_Format = timeFormat
        m_Time = DateTime.Now
        UpdateValue()

        'add message item
        AddItem(m_TimeItem)

    End Sub

    Private Sub UpdateValue()
        Select Case m_Format
            Case TimeFormat.A12
                m_TimeItem.Value = m_Time.ToString("yyMMddHHmmss")
            Case TimeFormat.A16
                m_TimeItem.Value = m_Time.ToString("yyyyMMddHHmmssff")
            Case TimeFormat.An
                Dim dto As DateTimeOffset = New DateTimeOffset(m_Time)
                m_TimeItem.Value = dto.ToString("yyyy-MM-ddTHH:mm:ss zzz")
        End Select

    End Sub

    Public Property Time() As DateTime
        Get
            Return m_Time
        End Get
        Set(ByVal value As DateTime)
            If m_Time <> value Then
                m_Time = value
                UpdateValue()
            End If
        End Set
    End Property
End Class
