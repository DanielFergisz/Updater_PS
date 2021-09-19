Imports System.IO

Public Class Form1
    Dim appPath As String = IO.Path.Combine(Application.StartupPath, "")
    Dim client As New Net.WebClient
    Dim newVersion As String = client.DownloadString("http://repairbox.pl/PS_OS/latestVersion.txt")

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Log.AppendText("Checking exe.. ")
        Timer1.Enabled = True
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False

        If File.Exists("PS_OS.exe") Then
            Log.SelectionColor = Color.ForestGreen
            Log.AppendText("OK")
            Log.SelectionColor = Color.Empty
            Log.AppendText(Environment.NewLine + "")
            Log.AppendText(Environment.NewLine + "Rename old version.. ")
            Timer2.Enabled = True
        Else
            Log.SelectionColor = Color.Red
            Log.AppendText("Failure !!!")
            Log.SelectionColor = Color.Empty
            Log.AppendText(Environment.NewLine + "")
            Log.AppendText(Environment.NewLine + "No application to update.")
        End If
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Timer2.Enabled = False
        My.Computer.FileSystem.RenameFile("PS_OS.exe", "PS_OS.exe.old")
        Log.SelectionColor = Color.ForestGreen
        Log.AppendText("OK")
        Log.SelectionColor = Color.Empty
        Log.AppendText(Environment.NewLine + "Download new version.. ")
        Timer3.Enabled = True
    End Sub

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        Timer3.Enabled = False
        client.DownloadFile("http://repairbox.pl/PS_OS/" + newVersion + "/PS_OS.exe", appPath + "\PS_OS.exe")
        client.Dispose()
        Log.SelectionColor = Color.ForestGreen
        Log.AppendText("OK")
        Log.SelectionColor = Color.Empty
        Timer4.Enabled = True
        Log.AppendText(Environment.NewLine + "Updating.. ")
    End Sub

    Private Sub Timer4_Tick(sender As Object, e As EventArgs) Handles Timer4.Tick
        Timer4.Enabled = False
        Log.SelectionColor = Color.ForestGreen
        Log.AppendText("OK")
        Log.SelectionColor = Color.Empty
        Log.AppendText(Environment.NewLine + "Update completed.")
        Log.AppendText(Environment.NewLine + "")
        Process.Start("PS_OS.exe")
        Log.AppendText(Environment.NewLine + "Restarting...")
        Timer5.Enabled = True
    End Sub

    Private Sub Timer5_Tick(sender As Object, e As EventArgs) Handles Timer5.Tick
        Timer5.Enabled = False
        For Each u As Process In Process.GetProcesses
            If u.ProcessName = "Updater_PS" Then
                u.Kill()
            End If
        Next
    End Sub
End Class
