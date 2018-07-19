Imports System.Collections.Generic
Imports System.IO

Public Class SECSGEM
    Inherits SECSMSG_Data

#Region "Variables"

#End Region

#Region "Constructor"
    Sub New(ByVal m_tmp() As Byte, ByVal _MessageType As TypeOfSECSMSG) 'recieve full message such as 0A 00 01 81 01 80 01 00 00 00 00 01 04
        MessageType = _MessageType
        If _MessageType = TypeOfSECSMSG.HSMS Then
            If m_tmp.Length < 14 Then Exit Sub
        End If
        ReceiveByte = m_tmp
        SECMessageClassify() 'assign value for every part of message
    End Sub
#End Region

End Class

Public Class SECSMSG_Data
    Inherits SECSMSG_Header

#Region "Variables"
    Private m_SMLOutput As String
    Private listOfObjSML As New List(Of ObjectSMLStructure)
    Private listOfSMLLayer As New List(Of objSMLLayer)
    Private _objSMLLayer As objSMLLayer
#End Region

#Region "Properties"
    Public Property SMLOutput() As String
        Get
            Return m_SMLOutput
        End Get
        Set(ByVal value As String)
            m_SMLOutput = value
        End Set
    End Property
#End Region

#Region "Structure"
    'Private Structure objSMLLayer
    '    Sub New(ByVal _LayerNo As Integer, ByVal _LevelItemQty As Integer, ByVal _LevelItemNo As Integer)
    '        m_LevelNo = _LayerNo
    '        m_LevelItemQty = _LevelItemQty
    '        m_LevelItemNo = _LevelItemNo
    '    End Sub

    '    Private m_LevelItemNo As Integer
    '    Public Property LevelItemNo() As Integer
    '        Get
    '            Return m_LevelItemNo
    '        End Get
    '        Set(ByVal value As Integer)
    '            m_LevelItemNo = value
    '        End Set
    '    End Property

    '    Private m_LevelItemQty As Integer
    '    Public Property LevelItemQty() As Integer
    '        Get
    '            Return m_LevelItemQty
    '        End Get
    '        Set(ByVal value As Integer)
    '            m_LevelItemQty = value
    '        End Set
    '    End Property

    '    Private m_LevelNo As Integer
    '    Public Property LevelNo() As Integer
    '        Get
    '            Return m_LevelNo
    '        End Get
    '        Set(ByVal value As Integer)
    '            m_LevelNo = value
    '        End Set
    '    End Property
    'End Structure
    'Private Structure ObjectSMLStructure

    '    Private m_Type As String
    '    Private m_Items As Integer
    '    Private m_Value As Object
    '    Private m_LevelItemNo As Integer
    '    Public Property LevelItemNo() As Integer
    '        Get
    '            Return m_LevelItemNo
    '        End Get
    '        Set(ByVal value As Integer)
    '            m_LevelItemNo = value
    '        End Set
    '    End Property

    '    Private m_LevelItemQty As Integer
    '    Public Property LevelItemQty() As Integer
    '        Get
    '            Return m_LevelItemQty
    '        End Get
    '        Set(ByVal value As Integer)
    '            m_LevelItemQty = value
    '        End Set
    '    End Property

    '    Private m_LevelNo As Integer
    '    Public Property LevelNo() As Integer
    '        Get
    '            Return m_LevelNo
    '        End Get
    '        Set(ByVal value As Integer)
    '            m_LevelNo = value
    '        End Set
    '    End Property

    '    Public Property ObjType() As String
    '        Get
    '            Return m_Type
    '        End Get
    '        Set(ByVal value As String)
    '            m_Type = value
    '        End Set
    '    End Property
    '    Public Property ObjItems() As Integer
    '        Get
    '            Return m_Items
    '        End Get
    '        Set(ByVal value As Integer)
    '            m_Items = value
    '        End Set
    '    End Property
    '    Public Property ObjValue() As Object
    '        Get
    '            Return m_Value
    '        End Get
    '        Set(ByVal value As Object)
    '            m_Value = value
    '        End Set
    '    End Property
    'End Structure
#End Region

#Region "SML Syntax"
    Private Function ConvertOctal(ByVal data As String) As String
        Dim str As String = ""

        Select Case data
            Case "00"
                str = "L"
            Case "10"
                str = "B"
            Case "11"
                str = "BOOLEAN"
            Case "20"
                str = "A"
            Case "21"
                str = "J"
            Case "22"
                str = "CHR"
            Case "30"
                str = "I8"
            Case "31"
                str = "I1"
            Case "32"
                str = "I2"
            Case "34"
                str = "I4"
            Case "40"
                str = "F8"
            Case "44"
                str = "F4"
            Case "50"
                str = "U8"
            Case "51"
                str = "U1"
            Case "52"
                str = "U2"
            Case "54"
                str = "U4"
        End Select

        Return str
    End Function

    Private Function RevertToOctal(ByVal data As String) As String
        Dim str As String = ""

        Select Case data
            Case "L"
                str = "00"
            Case "B"
                str = "10"
            Case "BOOLEAN"
                str = "11"
            Case "A"
                str = "20"
            Case "J"
                str = "21"
            Case "CHR"
                str = "22"
            Case "I8"
                str = "30"
            Case "I1"
                str = "31"
            Case "I2"
                str = "32"
            Case "I4"
                str = "34"
            Case "F8"
                str = "40"
            Case "F4"
                str = "44"
            Case "U8"
                str = "50"
            Case "U1"
                str = "51"
            Case "U2"
                str = "52"
            Case "U4"
                str = "54"

        End Select
        Return str
    End Function

    Private Function Type_LengthByte(ByVal Type As String) As Integer
        Dim str As Integer = 0

        Select Case Type
            Case "L"
                str = 0
            Case "B"
                str = 1
            Case "BOOLEAN"
                str = 1
            Case "A"
                str = 1
            Case "J"
                str = 1
            Case "CHR"
                str = 1
            Case "I8"
                str = 8
            Case "I1"
                str = 1
            Case "I2"
                str = 2
            Case "I4"
                str = 4
            Case "F8"
                str = 8
            Case "F4"
                str = 4
            Case "U8"
                str = 8
            Case "U1"
                str = 1
            Case "U2"
                str = 2
            Case "U4"
                str = 4

        End Select
        Return str
    End Function
#End Region

#Region "SML Manipulation"
    Private objLock As New Object
    Public Sub SMLConvert()
        Dim _objSMLLayer As New objSMLLayer(0, 1, 0)
        listOfSMLLayer.Add(_objSMLLayer)
        If Text IsNot Nothing AndAlso Text.Length <> 0 Then
            SyncLock objLock
                count = 0
                CreateSMLItems(Text)
                SMLOutput = MergeObjSML()
            End SyncLock
        Else
            SMLOutput = ""
        End If


    End Sub

    Private Function MergeObjSML() As String
        Dim ret As String = ""
        Dim _PrevLevelNo As Integer = 0
        Dim strTab As String = ""
        For Each obj As ObjectSMLStructure In listOfObjSML

            '----------------------------------------------
            'Addition for New SML format Improved
            If obj.LevelNo < _PrevLevelNo Then
                For j As Integer = _PrevLevelNo - 1 To obj.LevelNo Step -1
                    For i As Integer = 0 To j
                        ret += vbTab ' " "
                        'strTab += vbTab
                    Next
                    ret += ">" & vbCrLf

                Next
            End If
            _PrevLevelNo = obj.LevelNo
            '-----------------------------------------------

            For i As Integer = 0 To obj.LevelNo
                ret += vbTab ' " "
                'strTab += vbTab
            Next


            If obj.ObjType = "L" Then
                ret += "< " & obj.ObjType + " [" + obj.ObjItems.ToString + "]"
                If obj.ObjItems.ToString = "0" Then ret += " >"
            Else
                'ret += "< " & obj.ObjType + "(" + obj.ObjItems.ToString + ")" + "[" + obj.ObjValue.ToString + "] >"
                Select Case obj.ObjType
                    Case "A"
                        ret += "< " & obj.ObjType + " """ + obj.ObjValue.ToString + """ >"
                    Case "B"
                        ret += "< " & obj.ObjType + " " + obj.ObjValue.ToString + " >"
                    Case "BOOLEAN"
                        ret += "< " & obj.ObjType + " 0x" + obj.ObjValue.ToString + " >"
                    Case Else
                        ret += "< " & obj.ObjType + " " + obj.ObjValue.ToString + " >"
                End Select
            End If
            ret += vbCrLf

        Next

        '----------------------------------------------
        'Addition for New SML format Improved
        If 0 < _PrevLevelNo Then
            For j As Integer = _PrevLevelNo - 1 To 0 Step -1
                For i As Integer = 0 To j
                    ret += vbTab ' " "
                    'strTab += vbTab
                Next
                ret += ">" & vbCrLf

            Next
        End If
        ret = Mid(ret, 1, ret.Length - 2) & " ."
        '-----------------------------------------------
        Return ret
    End Function
    Private _objSMLStructure As ObjectSMLStructure
    Private containByte(0) As Byte
    Private LoopCount As Integer
    Dim count As Integer = 0
    Private Sub CreateSMLItems(ByVal _Byte() As Byte)
        Try
            'Array.Resize(containByte, _Byte.Length)
            'Array.Copy(_Byte, 0, containByte, 0, _Byte.Length)
            Dim str As String
            Dim byteLength As Byte
            Dim sLengthByte As String
            Dim strValue As String
            Dim len As Integer
            Dim strASC As String
            Dim startIndex As Integer


            Dim pointer As Integer = 0

            'Do Until containByte.Length = 0
            Do Until pointer = _Byte.Length
                '_Byte = containByte
                _objSMLStructure = New ObjectSMLStructure

                With _objSMLStructure
                    str = ""
                    'Convert HEX to Binary
                    str = Convert.ToString(_Byte(pointer), 2) 'Convert.ToString(_Byte(0), 2)
                    str = str.ToString.PadLeft(8, "0"c)
                    byteLength = Convert.ToByte(str.Substring(str.Length - 2, 2), 2)
                    str = CStr(Convert.ToByte(str.Substring(0, 6), 2))
                    str = Convert.ToString(CInt(str), 8)
                    str = str.ToString.PadLeft(2, "0"c)

                    .ObjType = ConvertOctal(str) 'send only 6 digit

                    sLengthByte = ""
                    For index As Integer = pointer + 1 To pointer + byteLength
                        sLengthByte += Convert.ToString(_Byte(index), 16).PadLeft(2, "0"c)
                    Next
                    .ObjItems = Convert.ToInt32(sLengthByte, 16)                    'convert the following byte to decimal (n value)

                    strValue = ""
                    count += 1
                    If .ObjType <> "L" Then
                        For j As Integer = pointer + 1 To pointer + .ObjItems            'keep all value
                            strValue = strValue & Convert.ToString(_Byte(j + byteLength), 16).PadLeft(2, "0"c)
                        Next
                        If strValue <> "" Then

                            len = 0
                            Dim strNum As String = ""
                            Select Case .ObjType
                                Case "L"
                                    .ObjValue = Nothing
                                Case "B"
                                    len = 2
                                    For j As Integer = 0 To strValue.Length - len Step len
                                        strNum = strNum & "0x" & strValue.Substring(j, len) & ","
                                    Next
                                    .ObjValue = strNum.Remove(strNum.Length - 1, 1)
                                Case "BOOLEAN"
                                    .ObjValue = strValue.ToUpper
                                Case "A"
                                    strASC = ""
                                    For j As Integer = 0 To strValue.Length - 2 Step 2
                                        If j <= strValue.Length - 4 Then
                                            If strValue.Substring(j, 4) = "0d0a" Then 'for carrier return and line feed display
                                                strASC = strASC & """ 0x0D 0x0A """
                                                j += 2
                                            ElseIf strValue.Substring(j, 4) = "0a0D" Then
                                                strASC = strASC & """ 0x0A 0x0D """
                                                j += 2
                                            Else
                                                strASC = strASC & Chr(Convert.ToInt32(strValue.Substring(j, 2), 16))  'covert hex to char
                                            End If
                                        Else
                                            If strValue.Substring(j, 2) = "0a" Then
                                                strASC = strASC & """ 0x0A """
                                            ElseIf strValue.Substring(j, 2) = "0d" Then
                                                strASC = strASC & """ 0x0D """
                                            Else
                                                strASC = strASC & Chr(Convert.ToInt32(strValue.Substring(j, 2), 16))  'covert hex to char
                                            End If
                                        End If
                                    Next
                                    .ObjValue = strASC
                                Case "J"
                                    .ObjValue = strValue
                                Case "CHR"
                                    .ObjValue = strValue
                                Case "I8"
                                    len = 16
                                    For j As Integer = 0 To strValue.Length - len Step len
                                        strNum = strNum & CStr(Convert.ToInt64(strValue.Substring(j, len), 16)) & ","
                                    Next
                                    .ObjValue = strNum.Remove(strNum.Length - 1, 1)
                                Case "I1"
                                    len = 2
                                    For j As Integer = 0 To strValue.Length - len Step len
                                        strNum = strNum & CStr(Convert.ToSByte(strValue.Substring(j, len), 16)) & ","
                                    Next
                                    .ObjValue = strNum.Remove(strNum.Length - 1, 1)
                                Case "I2"
                                    len = 4
                                    For j As Integer = 0 To strValue.Length - len Step len
                                        strNum = strNum & CStr(Convert.ToInt16(strValue.Substring(j, len), 16)) & ","
                                    Next
                                    .ObjValue = strNum.Remove(strNum.Length - 1, 1)
                                Case "I4"
                                    len = 8
                                    For j As Integer = 0 To strValue.Length - len Step len
                                        strNum = strNum & CStr(Convert.ToInt32(strValue.Substring(j, len), 16)) & ","
                                    Next
                                    .ObjValue = strNum.Remove(strNum.Length - 1, 1)
                                Case "F8"                             '160711 \783 F8 Convert
                                    len = 16
                                    For j As Integer = 0 To strValue.Length - len Step len
                                        strNum = strNum & CStr(ConvertHexToDouble(strValue.Substring(j, len))) & ","
                                    Next
                                    .ObjValue = strNum.Remove(strNum.Length - 1, 1)

                                Case "F4"                              '160711 \783 F8 Convert
                                    len = 8
                                    For j As Integer = 0 To strValue.Length - len Step len
                                        strNum = strNum & CStr(ConvertHexToSingle(strValue.Substring(j, len))) & ","
                                    Next
                                    .ObjValue = strNum.Remove(strNum.Length - 1, 1)

                                Case "U8"
                                    len = 16
                                    For j As Integer = 0 To strValue.Length - len Step len
                                        strNum = strNum & CStr(Convert.ToUInt64(strValue.Substring(j, len), 16)) & ","
                                    Next
                                    .ObjValue = strNum.Remove(strNum.Length - 1, 1)
                                Case "U1"
                                    len = 2
                                    For j As Integer = 0 To strValue.Length - len Step len
                                        strNum = strNum & CStr(Convert.ToByte(strValue.Substring(j, len), 16)) & ","
                                    Next
                                    .ObjValue = strNum.Remove(strNum.Length - 1, 1)
                                Case "U2"
                                    len = 4
                                    For j As Integer = 0 To strValue.Length - len Step len
                                        strNum = strNum & CStr(Convert.ToUInt16(strValue.Substring(j, len), 16)) & ","
                                    Next
                                    .ObjValue = strNum.Remove(strNum.Length - 1, 1)
                                Case "U4"
                                    len = 8
                                    For j As Integer = 0 To strValue.Length - len Step len
                                        strNum = strNum & CStr(Convert.ToUInt32(strValue.Substring(j, len), 16)) & ","
                                    Next
                                    .ObjValue = strNum.Remove(strNum.Length - 1, 1)
                                Case Else
                                    .ObjValue = strValue
                            End Select
                        Else
                            .ObjValue = ""
                        End If
                    End If

                    For i As Integer = listOfSMLLayer.Count - 1 To 0 Step -1  'check & assign Tree level
                        If listOfSMLLayer(i).LevelItemNo < listOfSMLLayer(i).LevelItemQty Then
                            .LevelNo = listOfSMLLayer(i).LevelNo
                            .LevelItemQty = listOfSMLLayer(i).LevelItemQty
                            .LevelItemNo = listOfSMLLayer(i).LevelItemNo + 1

                            _objSMLLayer = New objSMLLayer(.LevelNo, .LevelItemQty, .LevelItemNo)
                            listOfSMLLayer(i) = _objSMLLayer
                            Exit For
                        End If
                    Next
                    If .ObjType = "L" Then
                        _objSMLLayer = New objSMLLayer(.LevelNo + 1, .ObjItems, 0)
                        listOfSMLLayer.Add(_objSMLLayer)
                    End If

                    'prepare to recursive function
                    startIndex = 0
                    If .ObjType = "L" Then
                        If byteLength = 0 Then
                            startIndex = 2
                        Else
                            startIndex = byteLength + 1
                        End If

                    Else
                        startIndex = .ObjItems + byteLength + 1
                    End If
                    LoopCount += 1
                    'GC.Collect()
                    If LoopCount = 850 Then
                        'GC.Collect()
                        LoopCount = LoopCount
                    End If
                    pointer += startIndex

                    'Array.Resize(containByte, _Byte.Length - startIndex)
                    'Array.Copy(_Byte, startIndex, containByte, 0, containByte.Length)

                End With

                listOfObjSML.Add(_objSMLStructure)
                _objSMLStructure.Dispose()
                '_objSMLLayer.Dispose()

                If containByte.Length = 0 Then
                    Exit Sub
                Else
                    'CreateSMLItems(containByte)
                End If
            Loop

        Catch ex As Exception
            SaveData("ndllSMLConvertErr.log", Format(Now, "yyyy/MM/dd HH:mm:ss:fff") + " : " + ex.Message)
            SaveData("ndllSMLConvertErr.log", Format(Now, "yyyy/MM/dd HH:mm:ss:fff") + " : " + ConvToString(_Byte))
        End Try

    End Sub
    Private Sub CreateSMLItems_Backup(ByVal _Byte() As Byte)
        SaveData("ndllSMLConvert.log", Format(Now, "yyyy/MM/dd HH:mm:ss:fff") + ConvToString(_Byte))

        Array.Resize(containByte, _Byte.Length)
        Array.Copy(_Byte, 0, containByte, 0, _Byte.Length)
        Dim str As String
        Dim byteLength As Byte
        Dim sLengthByte As String
        Dim strValue As String
        Dim len As Integer
        Dim strASC As String
        Dim startIndex As Integer

        Do Until containByte.Length = 0

            _Byte = containByte
            _objSMLStructure = New ObjectSMLStructure

            With _objSMLStructure
                str = ""
                'Convert HEX to Binary
                str = Convert.ToString(_Byte(0), 2)
                str = str.ToString.PadLeft(8, "0"c)
                byteLength = Convert.ToByte(str.Substring(str.Length - 2, 2), 2)
                str = CStr(Convert.ToByte(str.Substring(0, 6), 2))
                str = Convert.ToString(CInt(str), 8)
                str = str.ToString.PadLeft(2, "0"c)

                .ObjType = ConvertOctal(str) 'send only 6 digit

                sLengthByte = ""
                For index As Integer = 1 To byteLength
                    sLengthByte += Convert.ToString(_Byte(index), 16).PadLeft(2, "0"c)
                Next
                .ObjItems = Convert.ToInt32(sLengthByte, 16)                    'convert the following byte to decimal (n value)

                strValue = ""
                count += 1
                If .ObjType <> "L" Then
                    For j As Integer = 1 To .ObjItems            'keep all value
                        strValue = strValue & Convert.ToString(_Byte(j + byteLength), 16).PadLeft(2, "0"c)
                    Next
                    If strValue <> "" Then

                        len = 0
                        Dim strNum As String = ""
                        Select Case .ObjType
                            Case "L"
                                .ObjValue = Nothing
                            Case "B"
                                .ObjValue = strValue.ToUpper
                            Case "BOOLEAN"
                                .ObjValue = strValue.ToUpper
                            Case "A"
                                strASC = ""
                                For j As Integer = 0 To strValue.Length - 2 Step 2
                                    If j <= strValue.Length - 4 Then
                                        If strValue.Substring(j, 4) = "0d0a" Then 'for carrier return and line feed display
                                            strASC = strASC & """ 0x0D 0x0A """
                                            j += 2
                                        ElseIf strValue.Substring(j, 4) = "0a0D" Then
                                            strASC = strASC & """ 0x0A 0x0D """
                                            j += 2
                                        Else
                                            strASC = strASC & Chr(Convert.ToInt32(strValue.Substring(j, 2), 16))  'covert hex to char
                                        End If
                                    Else
                                        If strValue.Substring(j, 2) = "0a" Then
                                            strASC = strASC & """ 0x0A """
                                        ElseIf strValue.Substring(j, 2) = "0d" Then
                                            strASC = strASC & """ 0x0D """
                                        Else
                                            strASC = strASC & Chr(Convert.ToInt32(strValue.Substring(j, 2), 16))  'covert hex to char
                                        End If
                                    End If
                                Next
                                .ObjValue = strASC
                            Case "J"
                                .ObjValue = strValue
                            Case "CHR"
                                .ObjValue = strValue
                            Case "I8"
                                len = 16
                                For j As Integer = 0 To strValue.Length - len Step len
                                    strNum = strNum & CStr(Convert.ToInt64(strValue.Substring(j, len), 16)) & ","
                                Next
                                .ObjValue = strNum.Remove(strNum.Length - 1, 1)
                            Case "I1"
                                len = 2
                                For j As Integer = 0 To strValue.Length - len Step len
                                    strNum = strNum & CStr(Convert.ToSByte(strValue.Substring(j, len), 16)) & ","
                                Next
                                .ObjValue = strNum.Remove(strNum.Length - 1, 1)
                            Case "I2"
                                len = 4
                                For j As Integer = 0 To strValue.Length - len Step len
                                    strNum = strNum & CStr(Convert.ToInt16(strValue.Substring(j, len), 16)) & ","
                                Next
                                .ObjValue = strNum.Remove(strNum.Length - 1, 1)
                            Case "I4"
                                len = 8
                                For j As Integer = 0 To strValue.Length - len Step len
                                    strNum = strNum & CStr(Convert.ToInt32(strValue.Substring(j, len), 16)) & ","
                                Next
                                .ObjValue = strNum.Remove(strNum.Length - 1, 1)
                            Case "F8"
                                .ObjValue = CStr(ConvertHexToDouble(strValue))
                            Case "F4"
                                .ObjValue = CStr(ConvertHexToSingle(strValue))
                            Case "U8"
                                len = 16
                                For j As Integer = 0 To strValue.Length - len Step len
                                    strNum = strNum & CStr(Convert.ToUInt64(strValue.Substring(j, len), 16)) & ","
                                Next
                                .ObjValue = strNum.Remove(strNum.Length - 1, 1)
                            Case "U1"
                                len = 2
                                For j As Integer = 0 To strValue.Length - len Step len
                                    strNum = strNum & CStr(Convert.ToByte(strValue.Substring(j, len), 16)) & ","
                                Next
                                .ObjValue = strNum.Remove(strNum.Length - 1, 1)
                            Case "U2"
                                len = 4
                                For j As Integer = 0 To strValue.Length - len Step len
                                    strNum = strNum & CStr(Convert.ToUInt16(strValue.Substring(j, len), 16)) & ","
                                Next
                                .ObjValue = strNum.Remove(strNum.Length - 1, 1)
                            Case "U4"
                                len = 8
                                For j As Integer = 0 To strValue.Length - len Step len
                                    strNum = strNum & CStr(Convert.ToUInt32(strValue.Substring(j, len), 16)) & ","
                                Next
                                .ObjValue = strNum.Remove(strNum.Length - 1, 1)
                            Case Else
                                .ObjValue = strValue
                        End Select
                    Else
                        .ObjValue = ""
                    End If
                End If

                For i As Integer = listOfSMLLayer.Count - 1 To 0 Step -1  'check & assign Tree level
                    If listOfSMLLayer(i).LevelItemNo < listOfSMLLayer(i).LevelItemQty Then
                        .LevelNo = listOfSMLLayer(i).LevelNo
                        .LevelItemQty = listOfSMLLayer(i).LevelItemQty
                        .LevelItemNo = listOfSMLLayer(i).LevelItemNo + 1

                        _objSMLLayer = New objSMLLayer(.LevelNo, .LevelItemQty, .LevelItemNo)
                        listOfSMLLayer(i) = _objSMLLayer
                        Exit For
                    End If
                Next
                If .ObjType = "L" Then
                    _objSMLLayer = New objSMLLayer(.LevelNo + 1, .ObjItems, 0)
                    listOfSMLLayer.Add(_objSMLLayer)
                End If

                'prepare to recursive function
                startIndex = 0
                If .ObjType = "L" Then
                    If byteLength = 0 Then
                        startIndex = 2
                    Else
                        startIndex = byteLength + 1
                    End If

                Else
                    startIndex = .ObjItems + byteLength + 1
                End If
                LoopCount += 1
                'GC.Collect()
                If LoopCount = 850 Then
                    'GC.Collect()
                    LoopCount = LoopCount
                End If
                Array.Resize(containByte, _Byte.Length - startIndex)
                Array.Copy(_Byte, startIndex, containByte, 0, containByte.Length)

            End With

            listOfObjSML.Add(_objSMLStructure)
            _objSMLStructure.Dispose()
            '_objSMLLayer.Dispose()

            If containByte.Length = 0 Then
                Exit Sub
            Else
                'CreateSMLItems(containByte)
            End If
        Loop


        'Catch ex As Exception
        '    SaveData ("SMLConvert.log",
        'End Try
    End Sub
    Private Function ConvertHexToSingle(ByVal hexValue As String) As Single
        Try
            Dim iInputIndex As Integer = 0
            Dim iOutputIndex As Integer = 0
            Dim bArray(3) As Byte

            For iInputIndex = 0 To hexValue.Length - 1 Step 2
                bArray(iOutputIndex) = Byte.Parse(hexValue.Chars(iInputIndex) & hexValue.Chars(iInputIndex + 1), Globalization.NumberStyles.HexNumber)
                iOutputIndex += 1
            Next

            Array.Reverse(bArray)

            Return BitConverter.ToSingle(bArray, 0)
        Catch ex As Exception
            Throw New FormatException("The supplied hex value is either empty or in an incorrect format. Use the following format: 00000000", ex)
        End Try

    End Function

    Private Function ConvertHexToDouble(ByVal hexValue As String) As Double
        Try
            Dim iInputIndex As Integer = 0
            Dim iOutputIndex As Integer = 0
            Dim bArray(7) As Byte

            For iInputIndex = 0 To hexValue.Length - 1 Step 2
                bArray(iOutputIndex) = Byte.Parse(hexValue.Chars(iInputIndex) & hexValue.Chars(iInputIndex + 1), Globalization.NumberStyles.HexNumber)
                iOutputIndex += 1
            Next

            Array.Reverse(bArray)

            Return BitConverter.ToDouble(bArray, 0)
        Catch ex As Exception
            Throw New FormatException("The supplied hex value is either empty or in an incorrect format. Use the following format: 00000000", ex)
        End Try

    End Function

    Private Function CutUnuseString(ByVal str As String) As String
        Try

            Dim ret As String = ""
            Dim indexStart As Integer = 0
            Dim len As Integer = 0
            indexStart = InStr(str, "<") + 1
            len = str.Length - indexStart
            str = Mid(str, indexStart, len)
            Return ret
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Function Separate_SML_Items(ByVal str As String) As String()
        Try

            'Dim ret As String = ""
            Dim split() As String = str.Split(CChar(" "))
            'If split.Length = 4 Then 'Normal situation

            'ElseIf split.Length > 4 Then 'Special situation ex. < A "ABCD EFG HIG " >

            'End If

            'Find Type of SML item
            Dim Type As String = ""
            'Dim indexStart As Integer = 0


            For i As Integer = 0 To split.Length - 1
                If split(i).Trim <> "" AndAlso split(i).Trim <> "<" Then
                    Type = split(i).Replace("<", "").Trim
                    If Type.StartsWith("L") = True Then
                        Type = "L"
                    End If
                    Exit For
                End If
            Next

            'For i As Integer = 0 To str.Length - 1
            '    If Mid(str, i, 1) <> "" Then
            '        indexStart = i
            '        Exit For
            '    End If
            'Next
            'split = Mid(str, indexStart, str.Length - indexStart).Split(" ")
            'Type = split(0)

            Dim Items As Integer = 0
            Dim sValue As String = ""
            Select Case Type
                Case "L"
                    Items = CInt(str.Substring(InStr(str, "["), InStr(str, "]") - InStr(str, "[") - 1))
                    sValue = ""
                Case "A"

                    Dim _split() As String = str.Split(CChar(""""))
                    For i As Integer = 1 To _split.Length - 2
                        sValue += _split(i) & """"
                    Next
                    sValue = sValue.Remove(sValue.Length - 1, 1)
                    'Modify


                    If sValue <> "" Then
                        sValue = sValue.Replace(""" 0x0D 0x0A """, "")
                        sValue = sValue.Replace(""" 0x0A 0x0D """, "")
                        sValue = sValue.Replace(""" 0x0D """, "").Replace(""" 0x0A """, "")
                        Items = sValue.Length
                    Else
                        Items = 0
                    End If

                Case "B"               '160707 \783 SML Convert tLib

                    If OprData.tLib Then
                        sValue = str.Substring(str.IndexOf("]") + 1).Replace("> .", "").Replace(">", "").Trim
                        sValue = sValue.Replace(" ", ",")


                    Else
                        sValue = Replace(str.Replace(Type, "").Replace("<", "").Replace("> .", "").Replace(">", ""), " ", "")
                        sValue = sValue.Replace("0x", "").Trim

                    End If
                    If sValue <> "" Then
                        If InStr(sValue, ",") > 0 Then
                            Items = (sValue.Split(CChar(",")).Length) * Type_LengthByte(Type)
                        Else
                            Items = Type_LengthByte(Type)
                        End If
                    Else
                        Items = 0
                    End If

                 

                Case "BOOLEAN", "TF"
                    sValue = Replace(str.Replace(Type, "").Replace("<", "").Replace("> .", "").Replace(">", ""), " ", "")

                    If OprData.tLib Then   '160707 \783 SML Convert tLib
                        Type = "BOOLEAN"
                        If sValue Like "*True" Then   'True = 01 False = 00 \783 160708
                            sValue = "01"
                        Else
                            sValue = "00"
                        End If
                       
                    End If

                    sValue = sValue.Replace("0x", "").Trim
                    If sValue <> "" Then
                        Items = Type_LengthByte(Type)
                    Else
                        Items = 0
                    End If


                Case Else

                    If OprData.tLib Then      '160707 \783 SML Convert tLib
                        sValue = str.Replace(Type, "").Replace("<", "").Replace("> .", "").Replace(">", "")
                        sValue = sValue.Substring(sValue.IndexOf("]") + 1).Trim.Replace(" ", ",")
                    Else
                        sValue = Replace(str.Replace(Type, "").Replace("<", "").Replace("> .", "").Replace(">", ""), " ", "").Trim
                    End If

                    If sValue <> "" Then
                        If InStr(sValue, ",") > 0 Then
                            Items = (sValue.Split(CChar(",")).Length) * Type_LengthByte(Type)
                        Else
                            Items = Type_LengthByte(Type)
                        End If
                    Else
                        Items = 0
                    End If

            End Select

            Dim ret(2) As String
            ret(0) = Type
            ret(1) = CStr(Items)
            ret(2) = sValue '.Trim
            Return ret
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function SMLRevert(ByVal tmp As String) As Byte()
        'Try
        Dim sr As New StringReader(tmp)
        Dim _sType As String
        Dim _sItems As String
        Dim _sValue As String
        Dim str As String
        Dim readLine As String
        Dim tmpArray1 As New List(Of Byte)

        Dim arr As String()
        While sr.Peek <> -1
            readLine = sr.ReadLine


            If Replace(readLine.Trim, " ", "") <> "" AndAlso Replace(readLine.Trim, " ", "") <> ">" AndAlso Replace(readLine.Trim, " ", "") <> ">." Then

                arr = Separate_SML_Items(readLine)
                _sType = arr(0)
                _sItems = arr(1)
                _sValue = arr(2)

                str = CStr(Convert.ToInt32(RevertToOctal(_sType), 8))
                str = Convert.ToString(CInt(str), 2)
                Dim sLength As String
                If CInt(_sItems) <= 255 Then
                    str = str & "01"
                    tmpArray1.Add(Convert.ToByte(str, 2))
                    tmpArray1.Add(CByte(CInt(_sItems)))
                ElseIf CInt(_sItems) <= 65535 Then
                    str = str & "10"
                    sLength = Convert.ToString(CLng(_sItems), 16).PadLeft(4, "0"c)
                    tmpArray1.Add(Convert.ToByte(str, 2))
                    For j As Integer = 0 To sLength.Length - 2 Step 2
                        tmpArray1.Add(Convert.ToByte(sLength.Substring(j, 2), 16))
                    Next
                Else
                    str = str & "11"
                    sLength = Convert.ToString(CLng(_sItems), 16).PadLeft(6, "0"c)
                    tmpArray1.Add(Convert.ToByte(str, 2))
                    For j As Integer = 0 To sLength.Length - 2 Step 2
                        tmpArray1.Add(Convert.ToByte(sLength.Substring(j, 2), 16))
                    Next
                End If

                If _sType = "L" Then Continue While

                If _sType = "A" OrElse _sType = "B" OrElse _sType = "BOOLEAN" Then
                    If _sType = "A" Then
                        For j As Integer = 0 To _sValue.Length - 1
                            tmpArray1.Add(CByte(Asc(_sValue.Substring(j, 1))))
                        Next
                    ElseIf _sType = "B" Then
                        Dim split() As String = _sValue.Split(CChar(","))
                        For i As Integer = 0 To split.Length - 1
                            If split(i) = "" Then Continue For
                            tmpArray1.Add(Convert.ToByte(split(i), 16))
                        Next

                    Else
                        tmpArray1.Add(Convert.ToByte(_sValue, 16))
                    End If
                ElseIf _sType = "F4" Then    '160708 \783 ConvertF4
                    Dim split() As String = _sValue.Split(CChar(","))
                    Dim bArray(3) As Byte
                    For i As Integer = 0 To split.Length - 1
                        If split(i) = "" Then Continue For
                        bArray = BitConverter.GetBytes(CSng(split(i).Trim))  'Single float to Binary Byte
                        For j = 3 To 0 Step -1
                            tmpArray1.Add((bArray(j)))
                        Next

                    Next

                ElseIf _sType = "F8" Then    '160708 \783 ConvertF8

                    Dim split() As String = _sValue.Split(CChar(","))

                    Dim bArray(7) As Byte
                    For i As Integer = 0 To split.Length - 1
                        If split(i) = "" Then Continue For
                        bArray = BitConverter.GetBytes(CDbl(split(i).Trim))  'Double float to Binary Byte
                        For j = 7 To 0 Step -1
                            tmpArray1.Add((bArray(j)))
                        Next

                    Next

                Else


                    Dim split() As String = _sValue.Split(CChar(","))     'Split value to array

                    For i As Integer = 0 To split.Length - 1
                        If split(i) = "" Then Continue For
                        str = Convert.ToString(CLng(split(i)), 16)
                        Select Case _sType
                            Case "I8"
                                str = str.ToString.PadLeft(8 * 2, "0"c)
                            Case "I1"
                                str = str.ToString.PadLeft(1 * 2, "0"c)
                            Case "I2"
                                str = str.ToString.PadLeft(2 * 2, "0"c)
                            Case "I4"
                                str = str.ToString.PadLeft(4 * 2, "0"c)
                                'Case "F8"                                        ' '160708 \783 ConvertF8
                                '    str = str.ToString.PadLeft(8 * 2, "0"c)
                                'Case "F4"                                        '160708 \783 ConvertF4
                                '    str = str.ToString.PadLeft(4 * 2, "0"c)   
                            Case "U8"
                                str = str.ToString.PadLeft(8 * 2, "0"c)
                            Case "U1"
                                str = str.ToString.PadLeft(1 * 2, "0"c)
                            Case "U2"
                                str = str.ToString.PadLeft(2 * 2, "0"c)
                            Case "U4"
                                str = str.ToString.PadLeft(4 * 2, "0"c)
                            Case Else
                                str = str.ToString.PadLeft(CInt(_sItems) * 2, "0"c)
                        End Select
                        For j As Integer = 0 To str.Length - 2 Step 2
                            tmpArray1.Add(Convert.ToByte(str.Substring(j, 2), 16))
                        Next
                    Next
                    'str = Convert.ToString(CInt(_sValue), 16)
                    'str = str.ToString.PadLeft(_sItems * 2, "0"c)
                    'Dim str3 As String = ""
                    'For j As Integer = 0 To str.Length - 2 Step 2
                    '    tmpArray1.Add(Convert.ToByte(str.Substring(j, 2), 16))
                    'Next
                End If
            End If

        End While
        Text = tmpArray1.ToArray
        Return Text
        'Catch ex As Exception
        '    SaveData(strFileSystemLog, Format(Now, "yyyy/MM/dd HH:mm:ss") + " System Error : " + ex.Message + "--> SMLRevert1()" + vbCrLf)
        '    MsgBox(ex.Message)
        '    Return ""
        'End Try
    End Function

#End Region

End Class

Public Class SECSMSG_Header


    Public Enum TypeOfSECSMSG
        HSMS
        SEC1
    End Enum

#Region "Variables"
    Private m_MessageType As TypeOfSECSMSG
    Private m_ReceiveByte() As Byte
    Private m_LTH() As Byte
    Private m_Header() As Byte
    Private m_Text() As Byte
    Private m_rBit As Boolean
    Private m_DeviceNo As Integer
    Private m_Stream As Byte
    Private m_Function As Byte
    Private m_wBit As Boolean
    Private m_eBit As Boolean
    Private m_PType As Byte
    Private m_SType As Byte
    Private m_BlockNo As Byte
    Private m_CheckSum(1) As Byte
#End Region

#Region "Properties"

    Public Property MessageType() As TypeOfSECSMSG
        Get
            Return m_MessageType
        End Get
        Set(ByVal value As TypeOfSECSMSG)
            m_MessageType = value
        End Set
    End Property

    Public Property ReceiveByte() As Byte()
        Get
            Return m_ReceiveByte
        End Get
        Set(ByVal value As Byte())
            m_ReceiveByte = value
        End Set
    End Property

    Public Property LTH() As Byte()
        Get
            Return m_LTH
        End Get
        Set(ByVal value() As Byte)
            m_LTH = value
        End Set
    End Property

    Public Property Header() As Byte()
        Get
            Return m_Header
        End Get
        Set(ByVal value() As Byte)
            m_Header = value
        End Set
    End Property

    Public Property Text() As Byte()
        Get
            Return m_Text
        End Get
        Set(ByVal value() As Byte)
            m_Text = value
        End Set
    End Property

    Public Property Sec_rBit() As Boolean
        Get
            Return m_rBit
        End Get
        Set(ByVal value As Boolean)
            m_rBit = value
        End Set
    End Property

    Public Property DeviceNo() As Integer
        Get
            Return m_DeviceNo
        End Get
        Set(ByVal value As Integer)
            m_DeviceNo = value
        End Set
    End Property

    Public Property Sec_Stream() As Byte
        Get
            Return m_Stream
        End Get
        Set(ByVal value As Byte)
            m_Stream = value
        End Set
    End Property

    Public Property Sec_Function() As Byte
        Get
            Return m_Function
        End Get
        Set(ByVal value As Byte)
            m_Function = value
        End Set
    End Property

    Public Property Sec_wBit() As Boolean
        Get
            Return m_wBit
        End Get
        Set(ByVal value As Boolean)
            m_wBit = value
        End Set
    End Property

    Public Property Sec_eBit() As Boolean
        Get
            Return m_eBit
        End Get
        Set(ByVal value As Boolean)
            m_eBit = value
        End Set
    End Property

    Public Property Sec_PType() As Byte
        Get
            Return m_PType
        End Get
        Set(ByVal value As Byte)
            m_PType = value
        End Set
    End Property

    Public Property Sec_SType() As Byte
        Get
            Return m_SType
        End Get
        Set(ByVal value As Byte)
            m_SType = value
        End Set
    End Property

    Public Property BlockNo() As Byte
        Get
            Return m_BlockNo
        End Get
        Set(ByVal value As Byte)
            m_BlockNo = value
        End Set
    End Property

    Public Property CheckSum() As Byte()
        Get
            Return m_CheckSum
        End Get
        Set(ByVal value As Byte())
            m_CheckSum = value
        End Set
    End Property

    Public Overrides Function ToString() As String
        Dim ret As String = ""
        For i As Integer = 0 To 3
            ret += System.Convert.ToString(LTH(i), 16).PadLeft(2, "0"c).PadRight(3, " "c)
        Next
        For i As Integer = 0 To 9
            ret += Header(i) & " "
        Next
        Return ret
    End Function
#End Region

#Region "Classify Received MSG"
    Protected Sub SECMessageClassify()
        Try

            'Assign LTH
            If m_MessageType = TypeOfSECSMSG.HSMS Then
                ReDim LTH(3)
                For i As Integer = 0 To 3
                    LTH(i) = m_ReceiveByte(i)
                Next
                'Search for Header
                'Assign Header
                ReDim Header(9)
                For i As Integer = 0 To 9
                    Header(i) = m_ReceiveByte(i + 4)
                Next
                DeviceNo = Header(1)
                'assign function
                Sec_Function = Header(3)
                Sec_SType = Header(5)

                'assign Data MSG array
                Array.Resize(Text, m_ReceiveByte.Length - 14)
                Array.Copy(m_ReceiveByte, 14, Text, 0, m_ReceiveByte.Length - 14)

            Else
                ReDim LTH(0)
                LTH(0) = m_ReceiveByte(0)
                'Search for Header
                'Assign Header
                ReDim Header(9)
                For i As Integer = 0 To 9
                    Header(i) = m_ReceiveByte(i + 1)
                Next
                If Header(0) >= 128 Then
                    Sec_rBit = True
                    DeviceNo = Convert.ToUInt16(Convert.ToString(Header(0) - 128, 16) + Convert.ToString(Header(1), 16), 16)
                Else
                    DeviceNo = Convert.ToUInt16(Convert.ToString(Header(0), 16) + Convert.ToString(Header(1), 16), 16)
                End If
                'assign ebit
                If Header(4) >= 128 Then
                    Sec_eBit = True
                End If
                'assign function
                Sec_Function = Header(3)
                BlockNo = Header(5)
                'assign Data MSG array
                Array.Resize(Text, m_ReceiveByte.Length - 13)
                Array.Copy(m_ReceiveByte, 11, Text, 0, m_ReceiveByte.Length - (13))

                Array.Copy(m_ReceiveByte, m_ReceiveByte.Length - 2, CheckSum, 0, 2)
            End If

            'assign wbit and stream
            If Header(2) >= 128 Then
                Sec_wBit = True
                Sec_Stream = CByte(Header(2) - 128)
            Else : Sec_wBit = False
                Sec_Stream = Header(2)
            End If

        Catch ex As Exception
            Throw ex 'SaveCatchLog(ex.Message, "SECMessageClassify()")
        End Try

    End Sub
#End Region


End Class

Friend Class ObjectSMLStructure
    Implements IDisposable
    Private m_Type As String
    Private m_Items As Integer
    Private m_Value As Object
    Private m_LevelItemNo As Integer
    Public Property LevelItemNo() As Integer
        Get
            Return m_LevelItemNo
        End Get
        Set(ByVal value As Integer)
            m_LevelItemNo = value
        End Set
    End Property
    Private m_LevelItemQty As Integer
    Public Property LevelItemQty() As Integer
        Get
            Return m_LevelItemQty
        End Get
        Set(ByVal value As Integer)
            m_LevelItemQty = value
        End Set
    End Property
    Private m_LevelNo As Integer
    Public Property LevelNo() As Integer
        Get
            Return m_LevelNo
        End Get
        Set(ByVal value As Integer)
            m_LevelNo = value
        End Set
    End Property
    Public Property ObjType() As String
        Get
            Return m_Type
        End Get
        Set(ByVal value As String)
            m_Type = value
        End Set
    End Property
    Public Property ObjItems() As Integer
        Get
            Return m_Items
        End Get
        Set(ByVal value As Integer)
            m_Items = value
        End Set
    End Property
    Public Property ObjValue() As Object
        Get
            Return m_Value
        End Get
        Set(ByVal value As Object)
            m_Value = value
        End Set
    End Property

    Private disposedValue As Boolean = False        ' To detect redundant calls
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free other state (managed objects).
            End If

            ' TODO: free your own state (unmanaged objects).
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

End Class
Friend Class objSMLLayer
    Implements IDisposable
    Sub New(ByVal _LayerNo As Integer, ByVal _LevelItemQty As Integer, ByVal _LevelItemNo As Integer)
        m_LevelNo = _LayerNo
        m_LevelItemQty = _LevelItemQty
        m_LevelItemNo = _LevelItemNo
    End Sub

    Private m_LevelItemNo As Integer
    Public Property LevelItemNo() As Integer
        Get
            Return m_LevelItemNo
        End Get
        Set(ByVal value As Integer)
            m_LevelItemNo = value
        End Set
    End Property

    Private m_LevelItemQty As Integer
    Public Property LevelItemQty() As Integer
        Get
            Return m_LevelItemQty
        End Get
        Set(ByVal value As Integer)
            m_LevelItemQty = value
        End Set
    End Property

    Private m_LevelNo As Integer
    Public Property LevelNo() As Integer
        Get
            Return m_LevelNo
        End Get
        Set(ByVal value As Integer)
            m_LevelNo = value
        End Set
    End Property

    Private disposedValue As Boolean = False        ' To detect redundant calls
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free other state (managed objects).
            End If

            ' TODO: free your own state (unmanaged objects).
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub
End Class