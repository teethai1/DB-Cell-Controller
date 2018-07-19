Imports System.Xml.Serialization

Public Class CellConObj          '160906 \783 Add Class


#Region "TDC Object ---------------"

    Private _Process As String
    Public Property Process() As String
        Get
            Return _Process
        End Get
        Set(ByVal value As String)
            _Process = value
        End Set
    End Property
    Private _MCNo As String
    Public Property MCNo() As String
        Get
            Return _MCNo
        End Get
        Set(ByVal value As String)
            _MCNo = value
        End Set
    End Property

    Private _LotStartTime As Date
    Public Property LotStartTime() As Date
        Get
            Return _LotStartTime
        End Get
        Set(ByVal value As Date)
            _LotStartTime = value
        End Set
    End Property

    Private _LotEndTime As Date
    Public Property LotEndTime() As Date
        Get
            Return _LotEndTime
        End Get
        Set(ByVal value As Date)
            _LotEndTime = value
        End Set
    End Property

    Private _LRReply As String
    Public Property LRReply() As String
        Get
            Return _LRReply
        End Get
        Set(ByVal value As String)
            _LRReply = value
        End Set
    End Property

    Private _LSReply As String
    Public Property LSReply() As String
        Get
            Return _LSReply
        End Get
        Set(ByVal value As String)
            _LSReply = value
        End Set
    End Property

    Private _LSMode As Integer
    Public Property LSMode() As Integer
        Get
            Return _LSMode
        End Get
        Set(ByVal value As Integer)
            _LSMode = value
        End Set
    End Property

    Private _LEReply As String
    Public Property LEReply() As String
        Get
            Return _LEReply
        End Get
        Set(ByVal value As String)
            _LEReply = value
        End Set
    End Property

    Private _LEMode As Integer
    Public Property LEMode() As Integer
        Get
            Return _LEMode
        End Get
        Set(ByVal value As Integer)
            _LEMode = value
        End Set
    End Property


    Private _TotalGoodPcs As String = "0"         'Total Good
    Public Property TotalGoodPcs() As String
        Get
            Return _TotalGoodPcs
        End Get
        Set(ByVal value As String)
            _TotalGoodPcs = value
        End Set
    End Property

    Private _TotalNGPcs As String = "0"            'Total Ng
    Public Property TotalNGPcs() As String
        Get
            Return _TotalNGPcs
        End Get
        Set(ByVal value As String)
            _TotalNGPcs = value
        End Set
    End Property



    Private _GoodCat1 As String = "0"            '170105 \783 \Config SECSGEM ID
    Public Property GoodCat1() As String
        Get
            Return _GoodCat1
        End Get
        Set(ByVal value As String)
            _GoodCat1 = value
        End Set
    End Property

    Private _GoodCat2 As String = "0"         '170105 \783 \Config SECSGEM ID
    Public Property GoodCat2() As String
        Get
            Return _GoodCat2
        End Get
        Set(ByVal value As String)
            _GoodCat2 = value
        End Set
    End Property
    Private _NGbin1 As String = "0"              '170105 \783 \Config SECSGEM ID
    Public Property NGbin1() As String
        Get
            Return _NGbin1
        End Get
        Set(ByVal value As String)
            _NGbin1 = value
        End Set
    End Property
    Private _NGbin2 As String = "0"         '170105 \783 \Config SECSGEM ID
    Public Property NGbin2() As String
        Get
            Return _NGbin2
        End Get
        Set(ByVal value As String)
            _NGbin2 = value
        End Set
    End Property
    Private _NGbin3 As String = "0"          '170105 \783 \Config SECSGEM ID
    Public Property NGbin3() As String
        Get
            Return _NGbin3
        End Get
        Set(ByVal value As String)
            _NGbin3 = value
        End Set
    End Property
    Private _NGbin4 As String = "0"       '170105 \783 \Config SECSGEM ID
    Public Property NGbin4() As String
        Get
            Return _NGbin4
        End Get
        Set(ByVal value As String)
            _NGbin4 = value
        End Set
    End Property
    Private _NGbin5 As String = "0"         '170105 \783 \Config SECSGEM ID
    Public Property NGbin5() As String
        Get
            Return _NGbin5
        End Get
        Set(ByVal value As String)
            _NGbin5 = value
        End Set
    End Property
    Private _NGbin6 As String = "0"        '170105 \783 \Config SECSGEM ID
    Public Property NGbin6() As String
        Get
            Return _NGbin6
        End Get
        Set(ByVal value As String)
            _NGbin6 = value
        End Set
    End Property





    Private _LotID As String
    Public Property LotID() As String
        Get
            Return _LotID
        End Get
        Set(ByVal value As String)
            _LotID = value
        End Set
    End Property


    Private _Package As String
    Public Property Package() As String
        Get
            Return _Package
        End Get
        Set(ByVal value As String)
            _Package = value
        End Set
    End Property

    Private _DeviceName As String
    Public Property DeviceName() As String
        Get
            Return _DeviceName
        End Get
        Set(ByVal value As String)
            _DeviceName = value
        End Set
    End Property


    Private _OPID As String
    Public Property OPID() As String
        Get
            Return _OPID
        End Get
        Set(ByVal value As String)
            _OPID = value
        End Set
    End Property


    Private _OPCheck As String
    Public Property OPCheck() As String
        Get
            Return _OPCheck
        End Get
        Set(ByVal value As String)
            _OPCheck = value
        End Set
    End Property
#End Region   '     ========= TDC Object

    'Private _WaferID As String()
    'Public Property WaferID() As String()
    '    Get
    '        Return _WaferID
    '    End Get
    '    Set(ByVal value As String())
    '        _WaferID = value

    '    End Set
    'End Property
    Private _WaferLotID As String
    Public Property WaferLotID() As String
        Get
            Return _WaferLotID
        End Get
        Set(ByVal value As String)
            _WaferLotID = value

        End Set
    End Property
    Private _CurrentWaferID As String
    Public Property CurrentWaferID() As String
        Get
            Return _CurrentWaferID
        End Get
        Set(ByVal value As String)
            _CurrentWaferID = value

        End Set
    End Property

    Private _CurrentWaferLotID As String
    Public Property CurrentWaferLotID() As String
        Get
            Return _CurrentWaferLotID
        End Get
        Set(ByVal value As String)
            _CurrentWaferLotID = value
        End Set
    End Property

    Private _CurrentLotNo As String
    Public Property CurrentLotNo() As String
        Get
            Return _CurrentLotNo
        End Get
        Set(ByVal value As String)
            _CurrentLotNo = value
        End Set
    End Property


    Private _WaferID As New List(Of String)
    Public Property WaferID() As List(Of String)
        Get
            Return _WaferID
        End Get
        Set(ByVal value As List(Of String))
            _WaferID = value

        End Set
    End Property
    Private _recipe As String
    Public Property Recipe() As String
        Get
            Return _recipe
        End Get
        Set(ByVal value As String)
            _recipe = value

        End Set
    End Property


    Private _INPUTQty As Integer
    Public Property INPUTQty() As Integer
        Get
            Return _INPUTQty
        End Get
        Set(ByVal value As Integer)
            _INPUTQty = value
        End Set
    End Property
    Private _PermitCheckResult As String
    Public Property PermitCheckResult() As String
        Get
            Return _PermitCheckResult
        End Get
        Set(ByVal value As String)
            _PermitCheckResult = value
        End Set
    End Property
    Private _MagazineNo As String
    Public Property MagazineNo() As String
        Get
            Return _MagazineNo
        End Get
        Set(ByVal value As String)
            _MagazineNo = value
        End Set
    End Property

    Private _QrData As String
    Public Property QrData() As String
        Get
            Return _QrData
        End Get
        Set(ByVal value As String)
            _QrData = value

        End Set
    End Property

    Private _FrameSeqNo_1st As String
    Public Property FrameSeqNo_1st() As String
        Get
            Return _FrameSeqNo_1st
        End Get
        Set(ByVal value As String)
            _FrameSeqNo_1st = value

        End Set
    End Property
    Private _FrameSeqNo_2nd As String
    Public Property FrameSeqNo_2nd() As String
        Get
            Return _FrameSeqNo_2nd
        End Get
        Set(ByVal value As String)
            _FrameSeqNo_2nd = value

        End Set
    End Property

    Private _FrameLotNo_1st As String
    Public Property FrameLotNo_1st() As String
        Get
            Return _FrameLotNo_1st
        End Get
        Set(ByVal value As String)
            _FrameLotNo_1st = value

        End Set
    End Property
    Private _FrameLotNo_2nd As String
    Public Property FrameLotNo_2nd() As String
        Get
            Return _FrameLotNo_2nd
        End Get
        Set(ByVal value As String)
            _FrameLotNo_2nd = value

        End Set
    End Property


    Private _FrameType_1st As String
    Public Property FrameType_1st() As String
        Get
            Return _FrameType_1st
        End Get
        Set(ByVal value As String)
            _FrameType_1st = value

        End Set
    End Property
    Private _FrameType_2nd As String
    Public Property FrameType_2nd() As String
        Get
            Return _FrameType_2nd
        End Get
        Set(ByVal value As String)
            _FrameType_2nd = value

        End Set
    End Property
    Private _PreformLotNo_1st As String
    Public Property PreformLotNo_1st() As String
        Get
            Return _PreformLotNo_1st
        End Get
        Set(ByVal value As String)
            _PreformLotNo_1st = value

        End Set
    End Property
    Private _PreformLotNo_2nd As String
    Public Property PreformLotNo_2nd() As String
        Get
            Return _PreformLotNo_2nd
        End Get
        Set(ByVal value As String)
            _PreformLotNo_2nd = value

        End Set
    End Property
    Private _PreforInputDate_1st As Date
    Public Property PreforInputDate_1st() As Date
        Get
            Return _PreforInputDate_1st
        End Get
        Set(ByVal value As Date)
            _PreforInputDate_1st = value

        End Set
    End Property
    Private _PreforInputDate_2nd As Date
    Public Property PreforInputDate_2nd() As Date
        Get
            Return _PreforInputDate_2nd
        End Get
        Set(ByVal value As Date)
            _PreforInputDate_2nd = value

        End Set
    End Property
    Private _PreforExpireDate_1st As Date
    Public Property PreforExpireDate_1st() As Date
        Get
            Return _PreforExpireDate_1st
        End Get
        Set(ByVal value As Date)
            _PreforExpireDate_1st = value

        End Set
    End Property
    Private _PreforExpireDate_2nd As Date
    Public Property PreforExpireDate_2nd() As Date
        Get
            Return _PreforExpireDate_2nd
        End Get
        Set(ByVal value As Date)
            _PreforExpireDate_2nd = value

        End Set
    End Property

    Private _PreformType_1st As String
    Public Property PreformType_1st() As String
        Get
            Return _PreformType_1st
        End Get
        Set(ByVal value As String)
            _PreformType_1st = value

        End Set
    End Property
    Private _PreformType_2nd As String
    Public Property PreformType_2nd() As String
        Get
            Return _PreformType_2nd
        End Get
        Set(ByVal value As String)
            _PreformType_2nd = value

        End Set
    End Property

    Private _PreformQR_1st As String
    Public Property PreformQR_1st As String
        Get
            Return _PreformQR_1st
        End Get
        Set(ByVal value As String)
            _PreformQR_1st = value

        End Set
    End Property

    Private _PreformQR_2nd As String
    Public Property PreformQR_2nd As String
        Get
            Return _PreformQR_2nd
        End Get
        Set(ByVal value As String)
            _PreformQR_2nd = value

        End Set
    End Property


    Private _ChipSizeX As Single
    Public Property ChipSizeX() As Single
        Get
            Return _ChipSizeX
        End Get
        Set(ByVal value As Single)
            _ChipSizeX = value
        End Set
    End Property

    Private _ChipSizeY As Single
    Public Property ChipSizeY() As Single
        Get
            Return _ChipSizeY
        End Get
        Set(ByVal value As Single)
            _ChipSizeY = value
        End Set
    End Property

    Private _BlockCheck As String
    Public Property BlockCheck() As String
        Get
            Return _BlockCheck
        End Get
        Set(ByVal value As String)
            _BlockCheck = value
        End Set
    End Property


    Private _TsukaigeNeedNo As Short
    Public Property TsukaigeNeedNo() As Short
        Get
            Return _TsukaigeNeedNo
        End Get
        Set(ByVal value As Short)
            _TsukaigeNeedNo = value
        End Set
    End Property


    Private _TsukaigePinStrock As Short
    Public Property TsukaigePinStrock() As Short
        Get
            Return _TsukaigePinStrock
        End Get
        Set(ByVal value As Short)
            _TsukaigePinStrock = value
        End Set
    End Property


    Private _TsukaigeMode As String
    Public Property TsukaigeMode() As String
        Get
            Return _TsukaigeMode
        End Get
        Set(ByVal value As String)
            _TsukaigeMode = value
        End Set
    End Property


    Private _PasteNozzleType As String
    Public Property PasteNozzleType() As String
        Get
            Return _PasteNozzleType
        End Get
        Set(ByVal value As String)
            _PasteNozzleType = value
        End Set
    End Property


    Private _PastNozzleNo As String
    Public Property PastNozzleNo() As String
        Get
            Return _PastNozzleNo
        End Get
        Set(ByVal value As String)
            _PastNozzleNo = value
        End Set
    End Property


    Private _PasteNozzleMode As String
    Public Property PasteNozzleMode() As String
        Get
            Return _PasteNozzleMode
        End Get
        Set(ByVal value As String)
            _PasteNozzleMode = value
        End Set
    End Property


    Private _RubberColletNo As Short
    Public Property RubberColletNo() As Short
        Get
            Return _RubberColletNo
        End Get
        Set(ByVal value As Short)
            _RubberColletNo = value
        End Set
    End Property


    Private _RubberMode As String
    Public Property RubberMode() As String
        Get
            Return _RubberMode
        End Get
        Set(ByVal value As String)
            _RubberMode = value
        End Set
    End Property


    Private _almPickup As Short
    Public Property AlarmPickup() As Short
        Get
            Return _almPickup
        End Get
        Set(ByVal value As Short)
            _almPickup = value
        End Set
    End Property


    Private _almPreform As Short
    Public Property AlarmPreform() As Short
        Get
            Return _almPreform
        End Get
        Set(ByVal value As Short)
            _almPreform = value
        End Set
    End Property

    Private _almBonder As Short
    Public Property AlarmBonder() As Short
        Get
            Return _almBonder
        End Get
        Set(ByVal value As Short)
            _almBonder = value
        End Set
    End Property

    Private _almFrameOut As Short
    Public Property AlarmFrameOut() As Short
        Get
            Return _almFrameOut
        End Get
        Set(ByVal value As Short)
            _almFrameOut = value
        End Set
    End Property

    Private _almBridgeInsp As Short
    Public Property AlarmBridgeInsp() As Short
        Get
            Return _almBridgeInsp
        End Get
        Set(ByVal value As Short)
            _almBridgeInsp = value
        End Set
    End Property

    Private _almPreformInsp As Short
    Public Property AlarmPreformInsp() As Short
        Get
            Return _almPreformInsp
        End Get
        Set(ByVal value As Short)
            _almPreformInsp = value
        End Set
    End Property

    Private _ReloadLot As String
    Public Property ReloadLot As String
        Get
            Return _ReloadLot
        End Get
        Set(ByVal value As String)
            _ReloadLot = value

        End Set
    End Property


    Private _CellConState As String
    Public Property CellConState() As String
        Get
            Return _CellConState
        End Get
        Set(ByVal value As String)
            _CellConState = value
        End Set
    End Property


    ''Tickness Preform

    Private _PreformThickness1 As Short
    Public Property PreformThickness1() As Short
        Get
            Return _PreformThickness1
        End Get
        Set(ByVal value As Short)
            _PreformThickness1 = value
        End Set
    End Property

    Private _PreformThickness2 As Short
    Public Property PreformThickness2() As Short
        Get
            Return _PreformThickness2
        End Get
        Set(ByVal value As Short)
            _PreformThickness2 = value
        End Set
    End Property

    Private _PreformThickness3 As Short
    Public Property PreformThickness3() As Short
        Get
            Return _PreformThickness3
        End Get
        Set(ByVal value As Short)
            _PreformThickness3 = value
        End Set
    End Property

    Private _PreformThickness4 As Short
    Public Property PreformThickness4() As Short
        Get
            Return _PreformThickness4
        End Get
        Set(ByVal value As Short)
            _PreformThickness4 = value
        End Set
    End Property

    Private _PreformThicknessAverage As Short
    Public Property PreformThicknessAverage() As Short
        Get
            Return _PreformThicknessAverage
        End Get
        Set(ByVal value As Short)
            _PreformThicknessAverage = value
        End Set
    End Property

    Private _PreformThicknessR As Short
    Public Property PreformThicknessR() As Short
        Get
            Return _PreformThicknessR
        End Get
        Set(ByVal value As Short)
            _PreformThicknessR = value
        End Set
    End Property

    Private _DoubleFrame As Short
    Public Property DoubleFrame() As Short
        Get
            Return _DoubleFrame
        End Get
        Set(ByVal value As Short)
            _DoubleFrame = value
        End Set
    End Property

    Private _FrameBent As Short
    Public Property FrameBent() As Short
        Get
            Return _FrameBent
        End Get
        Set(ByVal value As Short)
            _FrameBent = value
        End Set
    End Property

    Private _FrameBurn As Short
    Public Property FrameBurn() As Short
        Get
            Return _FrameBurn
        End Get
        Set(ByVal value As Short)
            _FrameBurn = value
        End Set
    End Property

    Private _BondingNG As Short
    Public Property BondingNG() As Short
        Get
            Return _BondingNG
        End Get
        Set(ByVal value As Short)
            _BondingNG = value
        End Set
    End Property


    Private _InputAdjust As Integer
    Public Property InputAdjust() As Integer
        Get
            Return _InputAdjust
        End Get
        Set(ByVal value As Integer)
            _InputAdjust = value
        End Set
    End Property

    Private _GoodAdjust As Integer
    Public Property GoodAdjust() As Integer
        Get
            Return _GoodAdjust
        End Get
        Set(ByVal value As Integer)
            _GoodAdjust = value
        End Set
    End Property

    Private _NGAdjust As Integer
    Public Property NGAdjust() As Integer
        Get
            Return _NGAdjust
        End Get
        Set(ByVal value As Integer)
            _NGAdjust = value
        End Set
    End Property

    Private _NoChip As Short
    Public Property NoChip() As Short
        Get
            Return _NoChip
        End Get
        Set(ByVal value As Short)
            _NoChip = value
        End Set
    End Property

    Private _TotalAlarm As Short
    Public Property TotalAlarm() As Short
        Get
            Return _TotalAlarm
        End Get
        Set(ByVal value As Short)
            _TotalAlarm = value
        End Set
    End Property



    Private _TotalGood As Integer
    Public Property TotalGood() As Integer
        Get
            Return _TotalGood
        End Get
        Set(ByVal value As Integer)
            _TotalGood = value
        End Set
    End Property

    Private _TotalNG As Integer
    Public Property TotalNG() As Integer
        Get
            Return _TotalNG
        End Get
        Set(ByVal value As Integer)
            _TotalNG = value
        End Set
    End Property

    Private _StateCellCon As Integer
    Public Property StateCellCon() As Integer
        Get
            Return _StateCellCon
        End Get
        Set(ByVal value As Integer)
            _StateCellCon = value
        End Set
    End Property

    Private _WaferNoList As List(Of String)
    Public Property WaferNoList() As List(Of String)
        Get
            Return _WaferNoList
        End Get
        Set(ByVal value As List(Of String))
            _WaferNoList = value
        End Set
    End Property

    Private _torinokoshi As Boolean
    Public Property Torinokoshi() As Boolean
        Get
            Return _torinokoshi
        End Get
        Set(ByVal value As Boolean)
            _torinokoshi = value
        End Set
    End Property


    Private _waferLotNolistfromDepyo As List(Of String)
    Public Property WaferLotNoFromDepyo() As List(Of String)
        Get
            Return _waferLotNolistfromDepyo
        End Get
        Set(ByVal value As List(Of String))
            _waferLotNolistfromDepyo = value
        End Set
    End Property

    Private _WaferLotNoListSplited As List(Of String)
    Public Property WaferLotNoListSplited() As List(Of String)
        Get
            Return _WaferLotNoListSplited
        End Get
        Set(ByVal value As List(Of String))
            _WaferLotNoListSplited = value
        End Set
    End Property

    Private _PCSPerFrame As Integer
    Public Property PCSPerFrame() As Integer
        Get
            Return _PCSPerFrame
        End Get
        Set(ByVal value As Integer)
            _PCSPerFrame = value
        End Set
    End Property

    Private _FrameCountTotal As Integer
    Public Property FrameCountTotal() As Integer
        Get
            Return _FrameCountTotal
        End Get
        Set(ByVal value As Integer)
            _FrameCountTotal = value
        End Set
    End Property


    Private _RunTimeCount As TimeSpan
    <XmlIgnore()> _
    Public Property RunTimeCount() As TimeSpan
        Get
            Return _RunTimeCount
        End Get
        Set(ByVal value As TimeSpan)
            _RunTimeCount = value
        End Set
    End Property
    Property RunTimeTick() As Long
        Get
            Return _RunTimeCount.Ticks
        End Get
        Set(ByVal value As Long)
            _RunTimeCount = TimeSpan.FromTicks(value)
        End Set
    End Property

    Private _StopTimeCount As TimeSpan
    <XmlIgnore()> _
    Public Property StopTimeCount() As TimeSpan
        Get
            Return _StopTimeCount
        End Get
        Set(ByVal value As TimeSpan)
            _StopTimeCount = value
        End Set
    End Property
    Property StopTimeTick() As Long
        Get
            Return _StopTimeCount.Ticks
        End Get
        Set(ByVal value As Long)
            _StopTimeCount = TimeSpan.FromTicks(value)
        End Set
    End Property

    Private _AlarmTimeCount As TimeSpan
    <XmlIgnore()> _
    Public Property AlarmTimeCount() As TimeSpan
        Get
            Return _AlarmTimeCount
        End Get
        Set(ByVal value As TimeSpan)
            _AlarmTimeCount = value
        End Set
    End Property

    Property AlarmTimeTick() As Long
        Get
            Return _AlarmTimeCount.Ticks
        End Get
        Set(ByVal value As Long)
            _AlarmTimeCount = TimeSpan.FromTicks(value)
        End Set
    End Property


    Private c_RubberColletID As String
    Public Property RubberColletID() As String
        Get
            Return c_RubberColletID
        End Get
        Set(ByVal value As String)
            c_RubberColletID = value
        End Set
    End Property


    Private c_Remark As String
    Public Property Remark() As String
        Get
            Return c_Remark
        End Get
        Set(ByVal value As String)
            c_Remark = value
        End Set
    End Property


End Class
