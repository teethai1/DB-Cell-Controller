Option Strict Off                                                           '160722 \783 common secs data type
Imports System.Data.SqlClient

Public Class SECSGEMDB

    Private m_ConnectionString As String

    Public Sub New(ByVal conStr As String)
        m_ConnectionString = conStr
    End Sub

    Public Function OpenConnection() As SqlConnection
        Dim con As SqlConnection = New SqlConnection()
        con.ConnectionString = m_ConnectionString
        con.Open()
        Return con
    End Function

    Public Sub FillDefinedReport(ByVal drDic As Dictionary(Of String, SecsDataType), ByVal con As SqlConnection, ByVal machineType As String) '160722 \783  common secs data type
        Using table As DataTable = New DataTable()
            Using cmd As SqlCommand = con.CreateCommand()
                'have to user "ORDER BY" as mr.Witoon old library
                cmd.CommandText = "SELECT ID, RPTID, VID, MachineType, Description FROM [SECSGEM].SECS.ReportAndElement WHERE MachineType = @MachineType ORDER BY RPTID, VID ASC"
                cmd.Parameters.AddWithValue("@MachineType", machineType)
                table.Load(cmd.ExecuteReader())
                drDic.Clear()
                Dim definedReport As New SecsDataType

                Dim vid As String
                Dim rptId As String        '160722 \783  common secs data type
                For Each row As DataRow In table.Rows
                    rptId = row("RPTID")
                    vid = row("VID")

                    If drDic.ContainsKey(rptId) Then
                        definedReport = drDic(rptId)
                    Else
                        definedReport = New SecsDataType
                        drDic.Add(rptId, definedReport)
                    End If
                    definedReport.VID.Add(vid)
                    definedReport.RPTID.Add(rptId)

                Next

                If drDic.Count > 0 Then
                    DefReport.Clear()
                    DefReport = table
                End If


            End Using
        End Using
    End Sub

    Public Sub FillLinkedReport(ByVal drDic As Dictionary(Of String, SecsDataType), ByVal con As SqlConnection, ByVal machineType As String)
        Using table As DataTable = New DataTable()
            Using cmd As SqlCommand = con.CreateCommand()
                cmd.CommandText = "SELECT ID, CEID, RPTID, MachineType, Description FROM [SECSGEM].SECS.CEIDAndReport WHERE MachineType = @MachineType ORDER BY CEID, RPTID ASC"
                cmd.Parameters.AddWithValue("@MachineType", machineType)
                table.Load(cmd.ExecuteReader())
                drDic.Clear()
                Dim linkedReport As New SecsDataType
                Dim ceid As String
                Dim rptId As String    '160722 \783  common secs data type

                For Each row As DataRow In table.Rows
                    ceid = row("CEID")
                    rptId = row("RPTID")
                    If drDic.ContainsKey(ceid) Then
                        linkedReport = drDic(ceid)
                    Else
                        linkedReport = New SecsDataType
                        drDic.Add(ceid, linkedReport)
                    End If
                    linkedReport.RPTID.Add(rptId)
                    linkedReport.CEID = ceid
                Next

                If drDic.Count > 0 Then
                    LinkTable.Clear()
                    LinkTable = table
                End If
            End Using
        End Using
    End Sub



    Public Sub LoadDataTableToLink(ByVal Dbl As DataTable)  '160722 \783  common secs data type

        Dim linkedReport As New SecsDataType
        Dim ceid As UInt32
        Dim rptId As String

        For Each row As DataRow In Dbl.Rows
            ceid = CUInt(row("CEID"))


            rptId = row("RPTID")

            If m_LinkedReportDic.ContainsKey(ceid) Then
                linkedReport = m_LinkedReportDic(ceid)
            Else
                linkedReport = New SecsDataType
                m_LinkedReportDic.Add(ceid, linkedReport)
            End If
            linkedReport.RPTID.Add(rptId)
            linkedReport.CEID = ceid
        Next

    End Sub


    Public Sub LoadDataTableToDefine(ByVal Dbl As DataTable)  '160722 \783  common secs data type

        Dim rptId As String
        Dim definedReport As New SecsDataType
        Dim vid As String

        For Each row As DataRow In Dbl.Rows
            rptId = (row("RPTID"))
            vid = CUInt(row("VID"))
            If m_DefinedReportDic.ContainsKey(rptId) Then
                definedReport = m_DefinedReportDic(rptId)
            Else
                definedReport = New SecsDataType
                m_DefinedReportDic.Add(rptId, definedReport)
            End If
            definedReport.VID.Add(vid)
            definedReport.RPTID.Add(rptId)
        Next


aaa:




    End Sub




End Class
