Public Class mp3Class : Implements IEquatable(Of mp3Class)



    Dim songDuration As String
    Dim songMP3URL As String
    Dim songSize As String
    Dim songExtension As String

    Dim songBitrate As String




    Public Sub New(ByVal songDuration As String, ByVal songMP3URL As String, ByVal songSize As String, ByVal songExtension As String, ByVal songBitrate As String)


        Me.songDuration = songDuration
        Me.songMP3URL = songMP3URL

        Me.songSize = songSize
        Me.songExtension = songExtension


        Me.songBitrate = songBitrate

    End Sub
    Public Overloads Function Equals(other As mp3Class) As Boolean _
                   Implements IEquatable(Of mp3Class).Equals
        If other Is Nothing Then Return False
        Return other.songMP3URL.Equals(Me.songMP3URL)
    End Function

    Public Function getSongDuration() As String
        Return Me.songDuration
    End Function
    Public Function getSongMP3URL() As String
        Return Me.songMP3URL
    End Function



    Public Function getSongSize() As String
        Return Me.songSize
    End Function
    Public Function getSongExtension() As String
        Return Me.songExtension
    End Function


    Public Function getSongBitrate() As String
        Return Me.songBitrate
    End Function

End Class
