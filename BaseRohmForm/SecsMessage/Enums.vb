#Region "State Models"

Public Enum ControlStateType As Byte
    OFFLINE = 0
    ONLINE_LOCAL = 1
    ONLINE_REMOTE = 2
    EQUIPMENT_OFFLINE = 3
    HOST_OFFLINE = 4
End Enum

Public Enum EquipmentStateEsec As Byte
    [None] = 0 ' i added this by myself 2015-07-02
    '2100HS
    HostNotReady = 154
    Executing = 211
    StoppedReady = 11
    NotInitializedNotReady = 111
    StartupNotReady = 112
    ShutdownNotReady = 113
    ProductionMenuNotReady = 121
    ProductionPrecheckNotReady = 122
    ProductionAccessNotReady = 123
    SetupKeyNotReady = 124
    SafetyNotReady = 131
    EquipmentNotReady = 141
    MaterialNotReady = 151
    ProcessNotReady = 152
    OperatorNotReady = 153

    '2009SSI
    Init = 1
    NotReady = 2
    Ready = 3
    MC_Executing = 4

End Enum

Public Enum EquipmentStateCanon
    'Canon D10R
    Undefined = 0
    INITIAL = 1
    IDEL = 2
    EXECUTING = 3
    TROUBLE = 4
    MAINTENANCE = 5
    PROC_CHANGE = 6
End Enum
Public Enum AlarmState
    AlarmSet = 1
    AlarmClear = 0
End Enum

#End Region

#Region "Custom Enum Addition"
Public Enum DoorState As Byte
    [None] = 0
    [Locked] = 1
    [Unlocked] = 2
End Enum
















#End Region


#Region "SECS-II data item list"


Public Enum HCACK As Byte
    '''0 - ok, completed
    OK = 0

    '''1 - invalid command
    InvalidCommand = 1

    '''2 - cannot do now
    CannotDoNow = 2

    '''3 - parameter error
    ParameterError = 3

    '''4 - initiated for asynchronous completion
    InitiatedForAsynCompletion = 4

    '''5 - rejected, already in desired condition
    RejectedAlreadyInDesired = 5

    '''6 - invalid object
    InvalidObject = 6

End Enum

Public Enum DRACK As Byte

    ''' <summary>
    ''' 0 - ok
    ''' </summary>
    ''' <remarks></remarks>
    OK = 0

    ''' <summary>
    ''' 1 - out of space
    ''' </summary>
    ''' <remarks></remarks>
    OutOfSpace = 1

    ''' <summary>
    ''' 2 - invalid format
    ''' </summary>
    ''' <remarks></remarks>
    InvalidFormat = 2

    ''' <summary>
    ''' 3 - 1 or more RPTID already defined
    ''' </summary>
    ''' <remarks></remarks>
    OneOrMoreRptidAlreadyDefined = 3

    ''' <summary>
    ''' '4 - 1 or more invalid VID
    ''' </summary>
    ''' <remarks></remarks>
    OneOrMoreInvalidVid = 4

End Enum




Public Enum LRACK As Byte

    ''' <summary>
    ''' 0 - ok
    ''' </summary>
    ''' <remarks></remarks>
    OK = 0

    ''' <summary>
    ''' 1 - out of space
    ''' </summary>
    ''' <remarks></remarks>
    OutOfSpace = 1

    ''' <summary>
    ''' 2 - invalid format
    ''' </summary>
    ''' <remarks></remarks>
    InvalidFormat = 2

    ''' <summary>
    ''' 3 - 1 or more CEID links already defined
    ''' </summary>
    ''' <remarks></remarks>
    OneOrMoreCeidLinksAlreadyDefined = 3

    ''' <summary>
    ''' 4 - 1 or more CEID invalid
    ''' </summary>
    ''' <remarks></remarks>
    OneOrMoreCeidInvalid = 4

    ''' <summary>
    ''' 5 - 1 or more RPTID invalid
    ''' </summary>
    ''' <remarks></remarks>
    OneOrMoreRptidInvalid = 5
End Enum

Public Enum ERACK As Byte

    ''' <summary>
    ''' '0 - ok
    ''' </summary>
    ''' <remarks></remarks>
    OK = 0

    ''' <summary>
    ''' 1 - denied
    ''' </summary>
    ''' <remarks></remarks>
    Denied = 1

End Enum

Public Enum TimeFormat
    A12 = 0
    A16 = 1
    An = 2
End Enum

Public Enum COMMACK As Byte
    OK = 0
    DENIED = 1
End Enum
Public Enum EAC As Byte
    OK = 0
    OneOrMoreConstantsDoesNotExist = 1
    BUSY = 2
    OneOrMoreValuesOutOfRange = 3
End Enum
Public Enum ACKC10 As Byte
    Accepted = 0
    MessageWillNotBeDisplayed = 1
    TerminalNotAvailable = 2

End Enum
Public Enum ACKC5 As Byte
    OK = 0
    DENIED = 1

End Enum

Public Enum ACKC7 As Byte
    Accepted
    PermissionNotGranted
    LengthError
    MatrixOverflow
    PPIDnotFound
    UnsupportedMode
    OtherError
End Enum

Public Enum SpoolCode As Byte
    Transmit
    Purge
End Enum
Public Enum RSDA As Byte
    ok
    RetryableBusy
    NoSpoolData

End Enum
Public Enum ONLACK As Byte
    Ok
    Refused
    AlreadyOnline

End Enum


Public Enum PPGNT As Byte        '160727 RecipeBodyManage
    Ok
    AlreadyHave
    NoSpace
    Invalid_PPID
    Busy_TryLater
    WillNotAccept
    OtherError
End Enum


Public Enum MAPER As Byte
    IDNoFound
    InvalidData
    FormatError
End Enum
#End Region