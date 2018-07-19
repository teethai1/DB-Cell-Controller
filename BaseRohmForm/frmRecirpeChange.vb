Public Class frmRecirpeChange
    Dim m_frmMain As ProcessForm
    Public m_NewRecipe As String
    Public m_CurrentRecipe As String
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If My.Settings.MCType = "Canon-D10R" Then
            m_frmMain.RemoteCMD_Remote()
        End If

        m_frmMain.RemotePP_SELECT(m_NewRecipe)
        Me.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Public Sub New(ByVal frmMain As ProcessForm)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_frmMain = frmMain
    End Sub

    Private Sub frmRecirpeChange_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lbmes.Text = "M/C Recipe Name :" & m_CurrentRecipe & "ต้องการเปลี่ยนไปเป็น" & vbCrLf & "Recipe Name: " & m_NewRecipe & " หรือไม่ ??"
    End Sub
End Class