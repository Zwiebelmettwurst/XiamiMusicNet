

Public Class SongClass : Implements IEquatable(Of SongClass)
    Dim songID As String
        Dim songName As String
    Dim songArtists As String
    Dim songAlbum As String
    Dim mp3details As List(Of mp3Class)

    Public Sub New(ByVal songID As String, ByVal songName As String, ByVal songArtists As String, ByVal songAlbum As String, ByVal mp3details As List(Of mp3Class))
        Me.songID = songID
        Me.songName = songName
        Me.songArtists = songArtists
        Me.songAlbum = songAlbum
        Me.mp3details = mp3details
    End Sub
    Public Overloads Function Equals(other As SongClass) As Boolean _
                   Implements IEquatable(Of SongClass).Equals
        If other Is Nothing Then Return False
        Return other.songID.Equals(Me.songID)
    End Function

    Public Function getSongID() As String
            Return Me.songID
        End Function
        Public Function getSongName() As String
            Return Me.songName
        End Function
    Public Function getSongArtists() As String
        Return Me.songArtists
    End Function

    Public Function getSongAlbum() As String
        Return Me.songAlbum
    End Function
    Public Function getMP3Details() As List(Of mp3Class)
        Return Me.mp3details
    End Function
End Class

