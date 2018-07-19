<Serializable()> _
Public Class Equipment         '160906 \783 Revise


#Region "===   SVID   ==="
    Private m_CurrentPPID As String
    Public Property CurrentPPID() As String
        Get
            Return m_CurrentPPID
        End Get
        Set(ByVal value As String)
            m_CurrentPPID = value
        End Set
    End Property

    Private m_ControlState As ControlStateType
    Public Property ControlState() As ControlStateType
        Get
            Return m_ControlState
        End Get
        Set(ByVal value As ControlStateType)
            m_ControlState = value

        End Set
    End Property
    Private m_EQStatus As EquipmentStateEsec
    Public Property EQStatus() As EquipmentStateEsec
        Get
            Return m_EQStatus
        End Get
        Set(ByVal value As EquipmentStateEsec)
            m_EQStatus = value
        End Set
    End Property

    Private _LotID As String            '170105 \783 \Config SECSGEM ID
    Public Property LotID() As String
        Get
            Return _LotID
        End Get
        Set(ByVal value As String)
            _LotID = value
        End Set
    End Property
    Private _GoodPcs As String
    Public Property GoodPcs() As String
        Get
            Return _GoodPcs
        End Get
        Set(ByVal value As String)
            _GoodPcs = value
        End Set
    End Property

    Private _GoodCat1 As String             '170105 \783 \Config SECSGEM ID
    Public Property GoodCat1() As String
        Get
            Return _GoodCat1
        End Get
        Set(ByVal value As String)
            _GoodCat1 = value
        End Set
    End Property

    Private _GoodCat2 As String             '170105 \783 \Config SECSGEM ID
    Public Property GoodCat2() As String
        Get
            Return _GoodCat2
        End Get
        Set(ByVal value As String)
            _GoodCat2 = value
        End Set
    End Property
    Private _NGbin1 As String              '170105 \783 \Config SECSGEM ID
    Public Property NGbin1() As String
        Get
            Return _NGbin1
        End Get
        Set(ByVal value As String)
            _NGbin1 = value
        End Set
    End Property
    Private _NGbin2 As String              '170105 \783 \Config SECSGEM ID
    Public Property NGbin2() As String
        Get
            Return _NGbin2
        End Get
        Set(ByVal value As String)
            _NGbin2 = value
        End Set
    End Property
    Private _NGbin3 As String              '170105 \783 \Config SECSGEM ID
    Public Property NGbin3() As String
        Get
            Return _NGbin3
        End Get
        Set(ByVal value As String)
            _NGbin3 = value
        End Set
    End Property
    Private _NGbin4 As String              '170105 \783 \Config SECSGEM ID
    Public Property NGbin4() As String
        Get
            Return _NGbin4
        End Get
        Set(ByVal value As String)
            _NGbin4 = value
        End Set
    End Property
    Private _NGbin5 As String              '170105 \783 \Config SECSGEM ID
    Public Property NGbin5() As String
        Get
            Return _NGbin5
        End Get
        Set(ByVal value As String)
            _NGbin5 = value
        End Set
    End Property
    Private _NGbin6 As String              '170105 \783 \Config SECSGEM ID
    Public Property NGbin6() As String
        Get
            Return _NGbin6
        End Get
        Set(ByVal value As String)
            _NGbin6 = value
        End Set
    End Property



    Private _NGPcs As String
    Public Property NGPcs() As String
        Get
            Return _NGPcs
        End Get
        Set(ByVal value As String)
            _NGPcs = value
        End Set
    End Property


    Private _AlarmID As String
    Public Property AlarmID() As String
        Get
            Return _AlarmID
        End Get
        Set(ByVal value As String)
            _AlarmID = value
        End Set
    End Property


#End Region

    Private m_EQStatusCanon As EquipmentStateCanon
    Public Property EQStatusCanon() As EquipmentStateCanon
        Get
            Return m_EQStatusCanon
        End Get
        Set(ByVal value As EquipmentStateCanon)
            m_EQStatusCanon = value
        End Set

    End Property


    Private _AlarmState As AlarmState
    Public Property AlarmState() As AlarmState
        Get
            Return _AlarmState
        End Get
        Set(ByVal value As AlarmState)
            _AlarmState = value
        End Set
    End Property



#Region "===   ECID   ==="
    Private m_DeviceId As UShort
    Public Property DeviceId() As UShort
        Get
            Return m_DeviceId
        End Get
        Set(ByVal value As UShort)
            m_DeviceId = value
        End Set
    End Property


#End Region



End Class
