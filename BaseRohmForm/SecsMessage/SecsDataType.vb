'This class for setting type of varible data use in SxFx.

Public Class SecsDataType

    Public RPTID As New List(Of UInt32)      'U4=UInt32, U2=UInt16   Software support only 2 Type
    Public VID As New List(Of UInt32)        'U4=UInt32, U2=UInt16   Software support only 2 Type
    Public CEID As UInt32                    'U4=UInt32, U2=UInt16   Software support only 2 Type
    Public DATAID As DATAIDType = DATAIDType.U4
    'DATAID = Used by: S2F33 S2F35 S2F39 S2F45 S2F49 S3F15 S3F17 S4F19 S4F25 S6F3 S6F5 S6F7 S6F8 S6F9 S6F11 S6F13 S6F16 S6F18 S6F25 S6F27 S13F11 S13F13 S13F15 S14F19 S14F21 S14F23 S15F1 S15F13 S15F15 S15F21 S15F23 S15F25 S15F27 S15F29 S15F33 S15F35 S15F39 S15F41 S15F43 S15F45 S15F47 S16F1 S16F3 S16F5 S16F11 S16F15 S17F1 S17F5 S17F9

    Public ECID As ECIDSecsForm = ECIDSecsForm.U4                    'U4=UInt32, U2=UInt16   Software support only 2 Type 
    '>>Used by: S2F13 S2F15 S2F29 S2F30

    Public Enum DATAIDType    'Coding support only these data types
        U1
        U2
        U4
    End Enum

End Class
Public Enum SecsFormat  '160823 \783 Addition SecsFormat
    Binary
    Bool
    Acsii
    I8
    I1
    I2
    I4
    F8
    F4
    U8
    U1
    U2
    U4
End Enum
Public Enum ECIDSecsForm   '161222 \783 Config SECSGEM
    U2
    U4
End Enum