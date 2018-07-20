Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Drawing
Imports System.Windows.Forms

Public Class Resize
    Private Structure ControlInfo
        Public name As String
        Public parentName As String
        Public leftOffsetPercent As Double
        Public topOffsetPercent As Double
        Public heightPercent As Double
        Public originalHeight As Integer
        Public originalWidth As Integer
        Public widthPercent As Single
        Public originalFontSize As Single
        Public originalcolumnWidth As Integer
        Public originalcolumnHight As Integer
    End Structure

    Private ctrlDict As Dictionary(Of String, ControlInfo) = New Dictionary(Of String, ControlInfo)()

    Public Sub FindAllControls(ByVal thisCtrl As Control)
        For Each ctl As Control In thisCtrl.Controls

            Try

                If (ctl.[GetType]() = GetType(DataGridView)) Then
                    Dim Grid As DataGridView = (CType((ctl), DataGridView))

                    If Not (ctl.Parent Is Nothing) Then
                        Dim parentHeight As Object = ctl.Parent.Height
                        Dim parentWidth As Object = ctl.Parent.Width
                        Dim c As ControlInfo = New ControlInfo()
                        c.name = Grid.Name
                        c.parentName = Grid.Parent.Name
                        c.topOffsetPercent = (Convert.ToDouble(Grid.Top) / Convert.ToDouble(parentHeight))
                        c.leftOffsetPercent = (Convert.ToDouble(Grid.Left) / Convert.ToDouble(parentWidth))
                        c.heightPercent = (Convert.ToDouble(Grid.Height) / Convert.ToDouble(parentHeight))
                        c.widthPercent = CSng((Convert.ToDouble(Grid.Width) / Convert.ToDouble(parentWidth)))
                        c.originalFontSize = Grid.DefaultCellStyle.Font.Size
                        c.originalHeight = Grid.Height
                        c.originalWidth = Grid.Width
                        ctrlDict.Add(c.name, c)
                    End If
                ElseIf Not (ctl.Parent Is Nothing) Then
                    Dim parentHeight As Object = ctl.Parent.Height
                    Dim parentWidth As Object = ctl.Parent.Width
                    Dim c As ControlInfo = New ControlInfo()
                    c.name = ctl.Name
                    c.parentName = ctl.Parent.Name
                    c.topOffsetPercent = (Convert.ToDouble(ctl.Top) / Convert.ToDouble(parentHeight))
                    c.leftOffsetPercent = (Convert.ToDouble(ctl.Left) / Convert.ToDouble(parentWidth))
                    c.heightPercent = (Convert.ToDouble(ctl.Height) / Convert.ToDouble(parentHeight))
                    c.widthPercent = CSng((Convert.ToDouble(ctl.Width) / Convert.ToDouble(parentWidth)))
                    c.originalFontSize = ctl.Font.Size
                    c.originalHeight = ctl.Height
                    c.originalWidth = ctl.Width
                    ctrlDict.Add(c.name, c)
                End If

            Catch ex As Exception
                Debug.Print(ex.Message)
            End Try

            If (ctl.Controls.Count > 0) Then
                Me.FindAllControls(ctl)
            End If
        Next
    End Sub

    Public Sub ResizeAllControls(ByVal thisCtrl As Control)
        Dim fontRatioW As Single
        Dim fontRatioH As Single
        Dim fontRatio As Single
        Dim f As Font

        For Each ctl As Control In thisCtrl.Controls

            Try

                If ctl.[GetType]() = GetType(PictureBox) Then

                    If Not (ctl.Parent Is Nothing) Then
                        Dim parentHeight As Integer = ctl.Parent.Height
                        Dim parentWidth As Integer = ctl.Parent.Width
                        Dim c As ControlInfo = New ControlInfo()
                        Dim ret As Boolean = False

                        Try
                            ret = ctrlDict.TryGetValue(ctl.Name, c)

                            If ret Then
                                ctl.Width = Convert.ToInt32(parentWidth * c.widthPercent)
                                ctl.Height = Convert.ToInt32(parentWidth * c.widthPercent / 1.47)
                                ctl.Top = Convert.ToInt32(parentHeight * c.topOffsetPercent)
                                ctl.Left = Convert.ToInt32(parentWidth * c.leftOffsetPercent)
                                f = ctl.Font
                                fontRatioW = (CSng(ctl.Width) / CSng(c.originalWidth))
                                fontRatioH = (CSng(ctl.Height) / CSng(c.originalHeight))
                                fontRatio = ((CSng(fontRatioW) + CSng(fontRatioH)) / CSng(2))
                                ctl.Font = New Font(f.FontFamily, (c.originalFontSize * fontRatio), f.Style)
                            End If

                        Catch __unusedException1__ As System.Exception
                        End Try
                    End If

                    GoTo NotResize
                End If

                If ctl.[GetType]().Name = "CircularButton" Then

                    If Not (ctl.Parent Is Nothing) Then
                        Dim parentHeight As Integer = ctl.Parent.Height
                        Dim parentWidth As Integer = ctl.Parent.Width
                        Dim c As ControlInfo = New ControlInfo()
                        Dim ret As Boolean = False

                        Try
                            ret = ctrlDict.TryGetValue(ctl.Name, c)

                            If ret Then
                                ctl.Width = Convert.ToInt32(parentHeight * c.heightPercent)
                                ctl.Height = Convert.ToInt32(parentHeight * c.heightPercent)
                                ctl.Top = Convert.ToInt32(parentHeight * c.topOffsetPercent)
                                ctl.Left = Convert.ToInt32(parentWidth * c.leftOffsetPercent)
                                f = ctl.Font
                                fontRatioW = (CSng(ctl.Width) / CSng(c.originalWidth))
                                fontRatioH = (CSng(ctl.Height) / CSng(c.originalHeight))
                                fontRatio = ((CSng(fontRatioW) + CSng(fontRatioH)) / CSng(2))
                                ctl.Font = New Font(f.FontFamily, (c.originalFontSize * fontRatio), f.Style)
                            End If

                        Catch __unusedException1__ As System.Exception
                        End Try
                    End If

                    GoTo NotResize
                End If

                If Not (ctl.Parent Is Nothing) Then
                    Dim parentHeight As Integer = ctl.Parent.Height
                    Dim parentWidth As Integer = ctl.Parent.Width
                    Dim c As ControlInfo = New ControlInfo()
                    Dim ret As Boolean = False

                    Try
                        ret = ctrlDict.TryGetValue(ctl.Name, c)

                        If ret Then
                            ctl.Width = Convert.ToInt32(parentWidth * c.widthPercent)
                            ctl.Height = Convert.ToInt32(parentHeight * c.heightPercent)
                            ctl.Top = Convert.ToInt32(parentHeight * c.topOffsetPercent)
                            ctl.Left = Convert.ToInt32(parentWidth * c.leftOffsetPercent)
                            f = ctl.Font
                            fontRatioW = (CSng(ctl.Width) / CSng(c.originalWidth))
                            fontRatioH = (CSng(ctl.Height) / CSng(c.originalHeight))
                            fontRatio = ((CSng(fontRatioW) + CSng(fontRatioH)) / CSng(2))
                            ctl.Font = New Font(f.FontFamily, (c.originalFontSize * fontRatio), f.Style)
                        End If

                    Catch __unusedException1__ As System.Exception
                    End Try
                End If

            Catch __unusedException1__ As Exception
            End Try

NotResize:

            If (ctl.Controls.Count > 0) Then
                Me.ResizeAllControls(ctl)
            End If
        Next
    End Sub
End Class
