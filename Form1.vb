Imports System.IO

Public Class Form1
    Dim appPath As String = IO.Path.Combine(Application.StartupPath, "")
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Log.AppendText("Checking exe.. ")
        Timer1.Enabled = True
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        Log.SelectionColor = Color.ForestGreen
        Log.AppendText("OK")
        Log.SelectionColor = Color.Empty
        If File.Exists("PS_OS.exe") Then
            Log.AppendText(Environment.NewLine + "Rename old file..")
            Threading.Thread.Sleep(500)
            My.Computer.FileSystem.RenameFile("PS_OS.exe", "PS_OS.exe.old")
            Dim client As New Net.WebClient
            Dim newVersion As String = client.DownloadString("http://dragondev.pl/apk/latestVersion.txt")
            Log.AppendText(Environment.NewLine + "Download new version...")
            Threading.Thread.Sleep(300)
            client.DownloadFile("http://dragondev.pl/apk/" + newVersion + "/PS_OS.exe", appPath + "\PS_OS.exe")
            client.Dispose()
            Threading.Thread.Sleep(3000)
            Process.Start("PS_OS.exe")
            Threading.Thread.Sleep(1000)
            For Each u As Process In Process.GetProcesses
                If u.ProcessName = "Updater_PS" Then
                    u.Kill()
                End If
            Next
        Else
            Log.AppendText(Environment.NewLine + "")
            Log.AppendText(Environment.NewLine + "No application to update.")

        End If
    End Sub
End Class
