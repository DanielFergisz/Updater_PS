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
            Log.AppendText(Environment.NewLine + "")
            Log.AppendText(Environment.NewLine + "Delete old firmware files.. ")

            If File.Exists("PS4\FULL\PS4UPDATE.PUP") Then
                File.Delete("PS4\FULL\PS4UPDATE.PUP")
            End If
            If File.Exists("PS4\UPDATE\PS4UPDATE.PUP") Then
                File.Delete("PS4\UPDATE\PS4UPDATE.PUP")
            End If
            If File.Exists("PS3\PS3UPDAT.PUP") Then
                File.Delete("PS3\PS3UPDAT.PUP")
            End If
            Log.SelectionColor = Color.ForestGreen
            Log.AppendText("OK")
            Log.SelectionColor = Color.Empty
            Log.AppendText(Environment.NewLine + "Rename old version..")
            Timer2.Enabled = True
        Else
            Log.AppendText(Environment.NewLine + "")
            Log.AppendText(Environment.NewLine + "No application to update.")

        End If
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Timer2.Enabled = False
        My.Computer.FileSystem.RenameFile("PS_OS.exe", "PS_OS.exe.old")
        Dim client As New Net.WebClient
        Dim newVersion As String = client.DownloadString("http://repairbox.pl/PS_OS/latestVersion.txt")
        Log.AppendText(Environment.NewLine + "Download new version...")
        Threading.Thread.Sleep(300)
        client.DownloadFile("http://repairbox.pl/PS_OS/" + newVersion + "/PS_OS.exe", appPath + "\PS_OS.exe")
        client.Dispose()
        Threading.Thread.Sleep(3000)
        Process.Start("PS_OS.exe")
        Threading.Thread.Sleep(1000)
        For Each u As Process In Process.GetProcesses
            If u.ProcessName = "Updater_PS" Then
                u.Kill()
            End If
        Next
    End Sub
End Class
