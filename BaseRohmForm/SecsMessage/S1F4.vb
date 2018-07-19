Imports XtraLibrary.SecsGem

Public Class S1F4
    Inherits SecsMessageBase

    Private m_Ln As SecsItemList

    Public Sub New()
        MyBase.New(1, 4, False)
        m_Ln = New SecsItemList("Ln")
        AddItem(m_Ln)

    End Sub

    Public ReadOnly Property SV As List(Of String)
        Get
            'For Each item As SecsItem In m_Ln.Value
            '    If item.Format = FormatCode.U4 Then
            '        Dim U4() As Integer = CType(item.Value, Integer())
            '        SVList.Add(CType(U4(0), String))
            '    ElseIf item.Format = FormatCode.ASCII Then
            '        SVList.Add(DirectCast(item.Value, String))
            '    Else
            '        SVList.Add("'" & item.Format.ToString & "' Not include Convert Code")
            '    End If

            'Next
            Dim SVList As New List(Of String)                        '160825 \783 Change to local sub varible
            For Each item As SecsItem In m_Ln.Value
                Dim str As String = item.Value.GetType.Name & " > This data type do not support in coding (SeeS2F14)"
                If item.Value.GetType.Name = "Byte[]" Then 'Binry,U1
                    Dim a = CType(item.Value, Byte())
                    For i = 0 To a.Count - 1
                        If i = 0 Then
                            str = CStr(a(i))
                        Else
                            str += "," & a(i)
                        End If

                    Next
                ElseIf item.Value.GetType.Name = "UInt64[]" Then 'U8
                    Dim a = CType(item.Value, UInt64())
                    For i = 0 To a.Count - 1
                        If i = 0 Then
                            str = CStr(a(i))
                        Else
                            str += "," & a(i)
                        End If

                    Next


                ElseIf item.Value.GetType.Name = "UInt32[]" Then 'U4
                    Dim a = CType(item.Value, UInt32())
                    For i = 0 To a.Count - 1
                        If i = 0 Then
                            str = CStr(a(i))
                        Else
                            str += "," & a(i)
                        End If

                    Next
                ElseIf item.Value.GetType.Name = "UInt16[]" Then 'U2
                    Dim a = CType(item.Value, UInt16())
                    For i = 0 To a.Count - 1
                        If i = 0 Then
                            str = CStr(a(i))
                        Else
                            str += "," & a(i)
                        End If

                    Next

                ElseIf item.Value.GetType.Name = "Int64[]" Then  'I8
                    Dim a = CType(item.Value, Int64())
                    For i = 0 To a.Count - 1
                        If i = 0 Then
                            str = CStr(a(i))
                        Else
                            str += "," & a(i)
                        End If

                    Next
                ElseIf item.Value.GetType.Name = "Int32[]" Then  'I4
                    Dim a = CType(item.Value, Int32())
                    For i = 0 To a.Count - 1
                        If i = 0 Then
                            str = CStr(a(i))
                        Else
                            str += "," & a(i)
                        End If

                    Next
                ElseIf item.Value.GetType.Name = "Int16[]" Then  'I2
                    Dim a = CType(item.Value, Int16())
                    For i = 0 To a.Count - 1
                        If i = 0 Then
                            str = CStr(a(i))
                        Else
                            str += "," & a(i)
                        End If

                    Next
                ElseIf item.Value.GetType.Name = "SByte[]" Then 'I1
                    Dim a = CType(item.Value, SByte())
                    For i = 0 To a.Count - 1
                        If i = 0 Then
                            str = CStr(a(i))
                        Else
                            str += "," & a(i)
                        End If

                    Next
                ElseIf item.Value.GetType.Name = "Boolean[]" Then 'Boolean

                    Dim a = CType(item.Value, Boolean())
                    str = a(0).ToString


                ElseIf item.Value.GetType.Name = "Double[]" Then  'F8
                    Dim a = CType(item.Value, Double())
                    For i = 0 To a.Count - 1
                        If i = 0 Then
                            str = CStr(a(i))
                        Else
                            str += "," & a(i)
                        End If

                    Next

                ElseIf item.Value.GetType.Name = "Single[]" Then  'F4
                    Dim a = CType(item.Value, Single())
                    For i = 0 To a.Count - 1
                        If i = 0 Then
                            str = CStr(a(i))
                        Else
                            str += "," & a(i)
                        End If

                    Next

                Else
                    str = CStr(item.Value)        'String
                End If


                SVList.Add(DirectCast(str, String))
            Next



            Return SVList
        End Get
    End Property

End Class
