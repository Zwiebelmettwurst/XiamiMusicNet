Option Strict On
Imports System.ComponentModel
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Text.RegularExpressions
Imports Newtonsoft.Json
Imports System.Web

Public Class Form1
    Dim mySong As SongClass
    Dim mymp3 As mp3Class
    Dim proxy As New List(Of WebProxy)
    Private cLvwSort As ListViewSort


    Private Sub btn_search_Click(sender As Object, e As EventArgs) Handles btn_search.Click
        lbl_status.Text = "searching..."
        Dim t As New Threading.Thread(AddressOf startQuery)
        t.IsBackground = True
        t.Start()

    End Sub

    Private Sub startQuery()
        lvw_results.Items.Clear()
        lvw_details.Items.Clear()
        Try

            Dim wc As New WebClient
        wc.Proxy = RandomProxy()

        Dim myquery As String = HttpUtility.UrlEncode(txt_search.Text, Encoding.GetEncoding("iso-8859-1"))

        Dim resultsource As String = wc.DownloadString("http://search.dongting.com/song/search/old?q=" & myquery & "&page=0&size=50")
        Dim resultsongs = Linq.JObject.Parse(resultsource)
        Dim mynewSongs As Linq.JToken = resultsongs.Item("data")
        Dim mySong As SongClass
        Dim resultlines As Integer = CInt(resultsongs.Item("rows"))
        If resultlines < 1 Then
                lbl_status.Text = "no results :("
                Exit Sub

        End If
            For Each songi In mynewSongs.Children

                Dim al As Linq.JToken = songi.Item("audition_list")

                Dim mp3list As New List(Of mp3Class)
                For Each mp3 In al
                    mp3list.Add(New mp3Class(mp3.Item("duration").ToString, mp3.Item("url").ToString, mp3.Item("size").ToString, mp3.Item("suffix").ToString, mp3.Item("bitRate").ToString))
                Next
                mySong = New SongClass(Unicode2UTF8(songi.Item("song_id").ToString), Unicode2UTF8(songi.Item("song_name").ToString), Unicode2UTF8(songi.Item("singer_name").ToString), Unicode2UTF8(songi.Item("album_name").ToString), mp3list)
                lvwAddItem(lvw_results, mySong, mySong.getSongName, mySong.getSongArtists, mySong.getSongAlbum)
            Next
            lbl_status.Text = "idle..."
        Catch ex As Exception
            lbl_status.Text = "Error! Please try again."
        End Try

    End Sub

    Private Sub lvwAddItem(ByVal lvw As ListView, ByVal mytag As Object, ByVal ParamArray Text() As String)

        With lvw

            .Items.Add(New ListViewItem(Text)).Tag = mytag
        End With
    End Sub
    Private Sub downloadProxy()
        Me.Enabled = False

        Dim wc As New WebClient
        Dim proxypage As String = wc.DownloadString("http://cn-proxy.com")
        '    Dim rx As New Regex("<tr>\n<td>([0-9.]*)<\/td>\n<td>([0-9]*)<\/td>\n<td>(.*)<\/td>\n<td>\n<div class=""graph""><strong class=""bar"" style=""width: ([0-9]+)%; background:#00dd00;""><span><\/span><\/strong><\/div>\n<\/td>\n<td>([0-9-.: ]+)<\/td>\n<\/tr>", RegexOptions.Multiline)

        Dim mmatch As MatchCollection = Regex.Matches(proxypage, "<tr>\n<td>([0-9.]*)<\/td>\n<td>([0-9]*)<\/td>\n<td>(.*)<\/td>\n<td>\n<div class=""graph""><strong class=""bar"" style=""width: ([0-9]+)%; background:#00dd00;""><span><\/span><\/strong><\/div>\n<\/td>\n<td>([0-9-.: ]+)<\/td>\n<\/tr>")

        Dim rxmatches As MatchCollection = Regex.Matches(proxypage, "<tr>\n<td>([0-9.]*)<\/td>\n<td>([0-9]*)<\/td>\n<td>(.*)<\/td>\n<td>\n<div class=""graph""><strong class=""bar"" style=""width: ([0-9]+)%; background:#00dd00;""><span><\/span><\/strong><\/div>\n<\/td>\n<td>([0-9-.: ]+)<\/td>\n<\/tr>") ', "<tr>\n<td>([0-9.]*)<\/td>\n<td>([0-9]*)<\/td>\n<td>(.*)<\/td>\n<td>\n<div class=""graph""><strong class=""bar"" style=""width: ([0-9]+)%; background:#00dd00;""><span><\/span><\/strong><\/div>\n<\/td>\n<td>([0-9-.: ]+)<\/td>\n<\/tr>")
        proxy.Clear()
        '    Dim sw As New StreamWriter(Application.StartupPath & "/Proxy.txt", False)

        For Each m As Match In mmatch
            proxy.Add(New WebProxy(m.Groups(1).ToString, CInt(m.Groups(2).ToString)))
            '  sw.WriteLine(m.Groups(1).ToString & ":" & m.Groups(2).ToString)

        Next
        '   chb_useproxy.Text = "Proxy (" & proxy.Count.ToString & ")"
        ' sw.Close()
        Me.Enabled = True

    End Sub
    Private Function RandomProxy() As WebProxy
        Dim rnd As New Random()
        Dim newproxy As WebProxy = proxy(rnd.Next(0, proxy.Count - 1))
        Return newproxy


    End Function
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckForIllegalCrossThreadCalls = False
        cLvwSort = New ListViewSort(lvw_results)
        Dim l As New Threading.Thread(AddressOf downloadProxy)
        l.IsBackground = True
        l.Start()


    End Sub

    Private Sub lvw_results_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvw_results.SelectedIndexChanged
        lvw_details.Items.Clear()
        GroupBox1.Visible = False

        If lvw_results.SelectedItems.Count = 1 Then

            mySong = CType(lvw_results.Items(lvw_results.SelectedIndices(0)).Tag, SongClass)
            For Each mp3 In mySong.getMP3Details
                lvwAddItem(lvw_details, mp3, mp3.getSongExtension, mp3.getSongBitrate, mp3.getSongDuration, mp3.getSongSize)
            Next


        End If

    End Sub

    Private Sub lvw_details_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvw_details.SelectedIndexChanged
        If lvw_details.SelectedItems.Count = 1 And Not IsNothing(mySong) Then
            GroupBox1.Visible = True
            mymp3 = CType(lvw_details.Items(lvw_details.SelectedIndices(0)).Tag, mp3Class)
        Else
            mymp3 = Nothing

            GroupBox1.Visible = False
        End If
        ProgressBar1.Value = 0
    End Sub
    Private Sub DownloadProgressCallback(ByVal sender As Object, ByVal e As DownloadProgressChangedEventArgs)

        ProgressBar1.Value = e.ProgressPercentage

    End Sub
    Private Sub btn_download_Click(sender As Object, e As EventArgs) Handles btn_download.Click
        If Not IsNothing(mySong) And Not IsNothing(mymp3) Then
            Dim sfd As New SaveFileDialog
            Dim wcdownload As New WebClient
            AddHandler wcdownload.DownloadProgressChanged, AddressOf DownloadProgressCallback

            With sfd
                .InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic)
                .FileName = mySong.getSongName.Replace(" ", "_") & "_-_" & mySong.getSongArtists.Replace(" ", "_") & "." & mymp3.getSongExtension

                If .ShowDialog = DialogResult.OK Then
                    wcdownload.DownloadFileAsync(New Uri(mymp3.getSongMP3URL), .FileName)


                End If

            End With

        End If
    End Sub
    Public Function Unicode2UTF8(ByVal strData As String) As String
        'http://www.utf8-zeichentabelle.de/unicode-utf8-table.pl?number=512&names=-&utf8=char
        Unicode2UTF8 = String.Empty
        If strData <> String.Empty Then
            Dim bytes() As Byte
            bytes = Encoding.GetEncoding("Windows-1252").GetBytes(strData)
            Unicode2UTF8 = Encoding.UTF8.GetString(bytes)
        End If
    End Function

    Private Sub btn_streamit_Click(sender As Object, e As EventArgs) Handles btn_streamit.Click
        If Not IsNothing(mymp3) Then
            AxWindowsMediaPlayer1.URL = mymp3.getSongMP3URL
        End If
    End Sub
End Class
