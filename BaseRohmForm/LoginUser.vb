
Public Class LoginUser

    Private Sub LoginUser_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        UserTable.TableName = "Userlogin"
        UserTable.WriteXmlSchema(My.Application.Info.DirectoryPath & "\UserLoginSchema.xml")
        UserTable.WriteXml(My.Application.Info.DirectoryPath & "\UserLogin.xml")


    End Sub


    Private Sub LoginUser_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try

            If UserTable.Columns.Count = 0 Then
                UserTable.Columns.Add("UserType", GetType(String))
                UserTable.Columns.Add("UserName", GetType(String))
                UserTable.Columns.Add("PassWord", GetType(String))
            End If

            DataGridView1.DataSource = UserTable
            ComboBox1.DataSource = [Enum].GetValues(GetType(CommonData.Level))    'Load from common Enum 160624 \783
            ComboBox1.SelectedIndex = 0

        Catch ex As Exception
            SaveCatchLog(ex.ToString, "LoginUser_Load()")
        End Try



    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Then
            Exit Sub
        End If

        If TextBox2.Text = "" Then
            Exit Sub
        End If
        For i = 0 To UserTable.Rows.Count - 1
            Dim r As DataRow = UserTable.Rows(i)
            If r.Item("UserName").ToString = TextBox1.Text And r.Item("UserType").ToString = ComboBox1.Text Then
                MsgBox("Duplicate UserName ")
                Exit Sub
            End If
        Next

        UserTable.Rows.Add(ComboBox1.Text, TextBox1.Text, TextBox2.Text)

    End Sub


    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        If TextBox1.Text = "" Then
            Exit Sub
        End If



        For i = 0 To UserTable.Rows.Count - 1
            Dim r As DataRow = UserTable.Rows(i)
            If r.Item("UserName").ToString = TextBox1.Text Then
                r.Delete()
                Exit For
            End If
        Next




    End Sub


End Class