Public Class ListViewSort

    Private WithEvents myLvw As ListView
    Private Frm As Form
    Private Sort_Order As SortOrder = SortOrder.Ascending
    Private m_SortByColumnClick As Boolean = True
    Private Last_Column As Int32 = -1
    Private m_SortOrderIcon As Boolean = True

    ''' <summary>
    ''' stellt Sort für das Listview zur Verfügung
    ''' </summary>
    ''' <param name="Lvw">erforderlich Listview</param>
    ''' <param name="SortByColumnClickAutomatic">automatischer Sort by ColumnClick</param>
    ''' <param name="SetNumericColumsRightAlign">numerische Spalten rechtsbündig ausrichten</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal Lvw As ListView,
                       Optional ByVal SortByColumnClickAutomatic As Boolean = True,
                       Optional ByVal SetNumericColumsRightAlign As Boolean = True)

        myLvw = Lvw
        Frm = Lvw.FindForm
        m_SortByColumnClick = SortByColumnClickAutomatic

        If SetNumericColumsRightAlign Then

            For i As Int32 = 0 To Lvw.Columns.Count - 1
                NumericRightAlign(i)
            Next

        End If

    End Sub

    ''' <summary>
    ''' ruft ab oder legt fest ob bei einem ColumnClick ein
    ''' automatischer Sort der jeweiligen Spalte ausgelöst wird:
    ''' bei Spaltenwechsel Ascending sonst Toggle Ascending-Descending:
    ''' default=True
    ''' </summary>
    Public Property SortByColumnClick() As Boolean
        Get
            SortByColumnClick = m_SortByColumnClick

        End Get

        Set(ByVal value As Boolean)
            m_SortByColumnClick = value

        End Set

    End Property

    ''' <summary>
    ''' eine oder alle numerischen Spalten rechtbündig setzen
    ''' </summary>
    ''' <param name="ColumnIndex">-1 = Alle </param>
    ''' <remarks></remarks>
    Public Sub NumericRightAlign(Optional ByVal ColumnIndex As Int32 = -1)

        If ColumnIndex = -1 Then

            For i As Int32 = 1 To myLvw.Columns.Count - 1

                If IsNumericColumn(i) Then
                    myLvw.Columns(i).TextAlign = HorizontalAlignment.Right
                End If

            Next

        Else

            If IsNumericColumn(ColumnIndex) Then
                myLvw.Columns(ColumnIndex).TextAlign = HorizontalAlignment.Right
            End If
        End If

    End Sub

    ''' <summary>
    ''' Sort einer Spalte (manuell) anstossen
    ''' </summary>
    ''' <param name="ColumnIndex">Index der Spalte 0-n</param>
    ''' <param name="s_Order">Ascending oder Descending, default = Automatic</param>
    ''' <remarks></remarks>
    Public Sub Sort(ByVal ColumnIndex As Int32, Optional ByVal s_Order As SortOrder = 0)

        Dim Col As Int32 = ColumnIndex

        ' Sortorder umkehren
        If Not Sort_Order = SortOrder.Ascending Then
            Sort_Order = SortOrder.Ascending

        Else

            Sort_Order = SortOrder.Descending
        End If

        ' bei Columnwechsel zuerst Ascending
        If Col <> Last_Column Then
            Sort_Order = SortOrder.Ascending
        End If

        If Not s_Order = SortOrder.None Then
            Sort_Order = s_Order
        End If

        ' Cursor saven und auf Hourglass
        Dim CC As Windows.Forms.Cursor = Frm.Cursor

        Frm.Cursor = Cursors.WaitCursor

        myLvw.ListViewItemSorter = Nothing

        ' Sorter starten je nach Spaltentyp Date, Numeric, String
        If IsEmptyColumn(Col) Then

            Dim x As Int32 = 0

            ' mach gar nix
        ElseIf IsDateColumn(Col) Then

            myLvw.ListViewItemSorter = New ListViewSorterDate(Col, Sort_Order)

        ElseIf IsNumericColumn(Col, False) Then

            myLvw.ListViewItemSorter = New ListViewSorterDecimal(Col, Sort_Order)

        Else

            myLvw.ListViewItemSorter = New ListViewSorterString(Col, Sort_Order)
        End If

        SortOrderIconShow(Col)

        Last_Column = Col
        Frm.Cursor = CC

    End Sub

    ''' <summary>
    ''' ruft ab oder legt fest ob ein SortOrderIcon angezeigt werden kann,
    ''' Vorraussetzung: eine ImageList mit Icons Key Ascending und Key Descending
    ''' ist mit der SmallImageList verknüpft, default=True
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SortOrderIcon() As Boolean
        Get
            SortOrderIcon = m_SortOrderIcon

        End Get

        Set(ByVal value As Boolean)
            m_SortOrderIcon = value

        End Set

    End Property

    ''' <summary>
    ''' zeigt im Columnheader ein Icon mit Key Ascending oder Key Descending an
    ''' aus der Imagelist mit Verknüpfung zur SmallImageList: default=automatisch über ColumnClick
    ''' </summary>
    Public Sub SortOrderIconShow(ByVal ColumnIndex As Int32)

        If (myLvw.SmallImageList Is Nothing) Or (Not SortOrderIcon) Then

            Exit Sub

        End If

        SortOrderIconRemove()

        If Sort_Order = SortOrder.Ascending Then
            myLvw.Columns(ColumnIndex).ImageKey = "Ascending"

        Else

            myLvw.Columns(ColumnIndex).ImageKey = "Descending"
        End If

    End Sub

    ''' <summary>
    ''' entfernt ein SortOrderIcon
    ''' </summary>
    Public Sub SortOrderIconRemove()

        For i As Int32 = 0 To myLvw.Columns.Count - 1

            With myLvw.Columns(i)

                If (.ImageKey = "Ascending") Or (.ImageKey = "Descending") Then
                    myLvw.Columns(i).ImageKey = Nothing
                    myLvw.Columns(i).ImageIndex = -1
                End If

            End With

        Next

    End Sub

    ''' <summary>
    ''' eine Spalte sortieren
    ''' </summary>
    Private Sub myLvw_ColumnClick(ByVal sender As Object, ByVal e As _
        System.Windows.Forms.ColumnClickEventArgs) Handles myLvw.ColumnClick

        ' ist automatische Sortierung eingestellt
        If SortByColumnClick Then
            Sort(e.Column)
        End If

    End Sub

    ''' <summary>
    ''' prüft eine Spalte auf rein Numeric, vorher sollte eine Prüfung auf Date erfolgen
    ''' </summary>
    Public ReadOnly Property IsNumericColumn(ByVal ColumnIndex As Int32, Optional ByVal _
        CheckDateBefore As Boolean = True) As Boolean

        Get

            If CheckDateBefore Then
                If IsDateColumn(ColumnIndex) Then
                    Return False
                End If
            End If

            Dim d As Decimal = Nothing
            Dim d1 As Date = Nothing

            For i As Int32 = 0 To myLvw.Items.Count - 1

                Dim s As String = Nothing

                If ColumnIndex = 0 Then
                    s = myLvw.Items(i).Text

                Else

                    If myLvw.Items(i).SubItems.Count > ColumnIndex Then
                        s = myLvw.Items(i).SubItems(ColumnIndex).Text
                    End If
                End If

                If Not s = Nothing Then
                    If Not Decimal.TryParse(s, d) Then
                        Return False
                    End If
                End If

            Next

            Return True

        End Get

    End Property

    ''' <summary>
    ''' prüft eine Spalte auf rein Date
    ''' </summary>
    Public ReadOnly Property IsDateColumn(ByVal ColumnIndex As Int32) As Boolean

        Get

            Dim d As Date

            For i As Int32 = 0 To myLvw.Items.Count - 1

                Dim s As String = Nothing

                If ColumnIndex = 0 Then
                    s = myLvw.Items(i).Text

                Else

                    If myLvw.Items(i).SubItems.Count > ColumnIndex Then
                        s = myLvw.Items(i).SubItems(ColumnIndex).Text
                    End If
                End If

                If Not s = Nothing Then
                    If Not Date.TryParse(s, d) Then
                        Return False
                    End If
                End If

            Next

            Return True

        End Get

    End Property

    ''' <summary>
    ''' prüft in einem Column ob alle Items leer sind
    ''' </summary>
    Public ReadOnly Property IsEmptyColumn(ByVal ColumnIndex As Int32) As Boolean

        Get

            For i As Int32 = 0 To myLvw.Items.Count - 1

                Dim s As String = Nothing

                If ColumnIndex = 0 Then
                    s = myLvw.Items(i).Text

                Else

                    If myLvw.Items(i).SubItems.Count > ColumnIndex Then
                        s = myLvw.Items(i).SubItems(ColumnIndex).Text
                    End If
                End If

                If Not s = Nothing Then
                    Return False
                End If

            Next

            Return True

        End Get

    End Property

End Class

''' <summary>
''' sortiert nach String, Aufruf nur aus ListViewSort
''' </summary>
''' <remarks></remarks>
Public Class ListViewSorterString

    Implements IComparer

    Private m_Col As Int32
    Private s_order As SortOrder
    Private x_Item As ListViewItem
    Private y_Item As ListViewItem

    Public Sub New(ByVal ColumnIndex As Int32, ByVal Sort_Order As SortOrder)

        m_Col = ColumnIndex
        s_order = Sort_Order

    End Sub

    Public Function Compare(ByVal x As Object, ByVal y As Object) _
                                As Integer Implements IComparer.Compare

        Dim xS As String = Nothing
        Dim yS As String = Nothing

        x_Item = DirectCast(x, ListViewItem)
        y_Item = DirectCast(y, ListViewItem)

        If m_Col = 0 Then
            xS = x_Item.Text
            yS = y_Item.Text

        Else

            If x_Item.SubItems.Count > m_Col Then
                xS = x_Item.SubItems(m_Col).Text
            End If

            If y_Item.SubItems.Count > m_Col Then
                yS = y_Item.SubItems(m_Col).Text
            End If
        End If

        If s_order = SortOrder.Ascending Then
            Return String.Compare(xS, yS)

        Else

            Return String.Compare(yS, xS)
        End If

    End Function

End Class

''' <summary>
''' Sortiert nach Date, Aufruf nur aus ListViewSort
''' </summary>
Public Class ListViewSorterDate

    Implements IComparer

    Private m_Col As Int32
    Private s_order As SortOrder
    Private x_Item As ListViewItem
    Private y_Item As ListViewItem

    Public Sub New(ByVal ColumnIndex As Int32, ByVal Sort_Order As SortOrder)

        m_Col = ColumnIndex
        s_order = Sort_Order

    End Sub

    Public Function Compare(ByVal x As Object, ByVal y As Object) _
                                As Integer Implements IComparer.Compare

        Dim xD As Date = Nothing
        Dim yD As Date = Nothing

        x_Item = DirectCast(x, ListViewItem)
        y_Item = DirectCast(y, ListViewItem)

        If m_Col = 0 Then
            Date.TryParse(x_Item.Text, xD)
            Date.TryParse(y_Item.Text, yD)

        Else

            If x_Item.SubItems.Count > m_Col Then
                Date.TryParse(x_Item.SubItems(m_Col).Text, xD)
            End If

            If y_Item.SubItems.Count > m_Col Then
                Date.TryParse(y_Item.SubItems(m_Col).Text, yD)
            End If
        End If

        If s_order = SortOrder.Ascending Then
            Return Date.Compare(xD, yD)

        Else

            Return Date.Compare(yD, xD)
        End If

    End Function

End Class

''' <summary>
''' Sortiert nach rein Decimal, Aufruf nur aus ListViewSort
''' </summary>
''' <remarks></remarks>
Public Class ListViewSorterDecimal

    Implements IComparer

    Private m_Col As Int32
    Private s_order As SortOrder
    Private x_Item As ListViewItem
    Private y_Item As ListViewItem

    Public Sub New(ByVal ColumnIndex As Int32, ByVal Sort_Order As SortOrder)

        m_Col = ColumnIndex
        s_order = Sort_Order

    End Sub

    Public Function Compare(ByVal x As Object, ByVal y As Object) _
                                As Integer Implements IComparer.Compare

        Dim xD As Decimal = Nothing
        Dim yD As Decimal = Nothing

        x_Item = DirectCast(x, ListViewItem)
        y_Item = DirectCast(y, ListViewItem)

        If m_Col = 0 Then
            Decimal.TryParse(x_Item.Text, xD)
            Decimal.TryParse(y_Item.Text, yD)

        Else

            If x_Item.SubItems.Count >= m_Col Then
                Decimal.TryParse(x_Item.SubItems(m_Col).Text, xD)
            End If

            If y_Item.SubItems.Count >= m_Col Then
                Decimal.TryParse(y_Item.SubItems(m_Col).Text, yD)
            End If
        End If

        If s_order = SortOrder.Ascending Then
            Return Decimal.Compare(xD, yD)

        Else

            Return Decimal.Compare(yD, xD)
        End If

    End Function

End Class

