Public Class LotInfomationEnd

    Private _totalgood As Integer
    Public Property TotalGood() As Integer
        Get
            Return _totalgood
        End Get
        Set(ByVal value As Integer)
            _totalgood = value
        End Set
    End Property

    Private _totalNG As Integer
    Public Property TotalNG() As Integer
        Get
            Return _totalNG
        End Get
        Set(ByVal value As Integer)
            _totalNG = value
        End Set
    End Property

    Private _RunningTime As String
    Public Property RunningTime() As String
        Get
            Return _RunningTime
        End Get
        Set(ByVal value As String)
            _RunningTime = value
        End Set
    End Property

    Private _stopTime As String
    Public Property StopTime() As String
        Get
            Return _stopTime
        End Get
        Set(ByVal value As String)
            _stopTime = value
        End Set
    End Property

    Private _alarmTime As String
    Public Property AlarmTime() As String
        Get
            Return _alarmTime
        End Get
        Set(ByVal value As String)
            _alarmTime = value
        End Set
    End Property

    Private _nochip As String
    Public Property NoChip() As String
        Get
            Return _nochip
        End Get
        Set(ByVal value As String)
            _nochip = value
        End Set
    End Property


    Private _almBrigdeInsp As String
    Public Property AlmBrigdeInsp() As String
        Get
            Return _almBrigdeInsp
        End Get
        Set(ByVal value As String)
            _almBrigdeInsp = value
        End Set
    End Property

    Private _almpickup As String
    Public Property AlmPickup() As String
        Get
            Return _almpickup
        End Get
        Set(ByVal value As String)
            _almpickup = value
        End Set
    End Property

    Private _mtbf As String
    Public Property MTBF() As String
        Get
            Return _mtbf
        End Get
        Set(ByVal value As String)
            _mtbf = value
        End Set
    End Property

    Private _mttr As String
    Public Property MTTR() As String
        Get
            Return _mttr
        End Get
        Set(ByVal value As String)
            _mttr = value
        End Set
    End Property

    Private _oprate As String
    Public Property OPRate() As String
        Get
            Return _oprate
        End Get
        Set(ByVal value As String)
            _oprate = value
        End Set
    End Property


    Private _rpm As String
    Public Property RPM() As String
        Get
            Return _rpm
        End Get
        Set(ByVal value As String)
            _rpm = value
        End Set
    End Property

    Private _stdrpm As String
    Public Property StandardRPM() As String
        Get
            Return _stdrpm
        End Get
        Set(ByVal value As String)
            _stdrpm = value
        End Set
    End Property


End Class
