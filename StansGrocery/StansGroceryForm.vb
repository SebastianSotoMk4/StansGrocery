'Sebastian Soto
'RCET0265
'Fall 2021
'Stan's Grocery
'https://github.com/SebastianSotoMk4/StansGrocery.git
'Instructinos 
'https://github.com/rosstimo/StansGrocery
Option Strict On
Option Explicit On
Public Class StansGroceryForm
    Dim fileName As String = "..\..\Grocery.txt"
    Dim groceryData(300, 2) As String
    Dim cleanData(300, 2) As String

    'sets defaults when loaded or called
    Sub Defaults()
        SearchTextBox.Text = Nothing
        CategoryRadioButton.Checked = True
        FilterComboBox.Text = (" Show All")
        SearchButton_Click()
    End Sub

    'keep
    Private Sub SearchButton_Click() Handles SearchButton.Click
        'FilterComboBox.Text = " Show All"
        CategoryRadioButton.Checked = True
        If SearchTextBox.Text = "zzz" Then
            Me.Close()
        Else
            ReadArray(SearchTextBox.Text)
        End If
        Select Case ProductListBox.Items.Count
            Case < 1
                Label3.Text = $"Sorry no matches for '{SearchTextBox.Text}'"
            Case = 1
                Label3.Text = $"{ProductListBox.Items.Count} product"
            Case >= 2
                Label3.Text = $"{ProductListBox.Items.Count} products"
        End Select

    End Sub


    Function DisplayLOC(SearchWord As String) As String
        Dim desiredItemLoc As String
        Dim loopNumber As Integer = 0

        Do Until desiredItemLoc = SearchWord
            Try

                desiredItemLoc = cleanData(loopNumber, 1)
                loopNumber += 1
            Catch ex As Exception

            End Try



        Loop
        loopNumber = 0
        Me.Text = desiredItemLoc
        Return desiredItemLoc
        desiredItemLoc = ""
    End Function


    Sub ReadArray(searchWord As String)
        Dim tempItem() As String
        Dim jankRecord As String
        Dim tempLoc() As String
        Dim tempCat() As String

        ProductListBox.Items.Clear()

        For i = LBound(Me.groceryData) To UBound(Me.groceryData)
            jankRecord = ($"{groceryData(i, 0)} {groceryData(i, 1)} {groceryData(i, 2)} ")

            tempItem = Split(groceryData(i, 0), "$$ITM")
            tempLoc = Split(groceryData(i, 1), "##LOC")
            tempCat = Split(groceryData(i, 2), "%%CAT")
            Try
                Console.WriteLine(tempItem(1))
                If InStr(jankRecord, searchWord, CompareMethod.Text) > 0 And InStr(jankRecord, searchWord, CompareMethod.Text) <> 0 Then
                    ProductListBox.Items.Add($"{tempItem(1)}")
                    Me.cleanData(i, 0) = tempItem(1)
                    Me.cleanData(i, 1) = tempLoc(1)
                    Me.cleanData(i, 2) = tempCat(1)

                End If
            Catch ex As Exception
                Console.WriteLine("oops")
            End Try
        Next
        ProductListBox.Sorted = True
        ProductListBox.Items.Remove("")

    End Sub


    Sub ReadFile()
        'Clear listbox each time called for neatness
        ProductListBox.Items.Clear()
        Dim currentLine As String
        Dim itemNo As Integer
        Try
            FileOpen(1, Me.fileName, OpenMode.Input)
            Do Until EOF(1)
                For lineNumber = 0 To 2

                    Input(1, currentLine)
                    Me.groceryData(itemNo, lineNumber) = currentLine

                Next
                itemNo += 1
            Loop
            FileClose(1)
        Catch ex As Exception

            If OpenFileDialog.ShowDialog() <> DialogResult.Cancel Then
                Me.fileName = OpenFileDialog.FileName
                ReadFile()
            Else
                Me.Close()
            End If
        End Try
    End Sub


    Private Sub StansGroceryForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ReadFile()
        Defaults()
    End Sub


    'Check Speling Deimiter
    Private Sub FilterSelect() Handles AisleRadioButton.CheckedChanged, CategoryRadioButton.CheckedChanged
        Dim temp() As String
        Dim delimiter As String
        Dim index As Integer
        Select Case True
            Case AisleRadioButton.Checked
                delimiter = "##LOC"
                index = 1

            Case CategoryRadioButton.Checked
                delimiter = "%%CAT"
                index = 2
        End Select
        FilterComboBox.Items.Clear()
        FilterComboBox.Items.Add(" Show All")
        FilterComboBox.SelectedItem = " Show All"

        For i = LBound(groceryData) To UBound(groceryData)
            Try
                temp = Split(groceryData(i, index), delimiter)
                If FilterComboBox.Items.Contains(temp(1)) = False And temp(1) <> "" Then   '.PadLeft(2)) = False And temp(1) <> "" Then
                    FilterComboBox.Items.Add(temp(1)) '.PadLeft(2))

                End If
            Catch ex As Exception

            End Try
        Next
        FilterComboBox.Sorted = True
    End Sub


    Private Sub FilterComboBox_SelectedIndexChanged() Handles FilterComboBox.SelectedIndexChanged
        '
        If CategoryRadioButton.Checked = True Then
            Label3.Text = $"{FilterComboBox.Text} products"
        End If
        If AisleRadioButton.Checked = True And FilterComboBox.Text <> "Show All" Then
            Label3.Text = $"{FilterComboBox.Text} Aisle products  "
        End If

        If FilterComboBox.Text = " Show All" Then
            ReadArray(" ")
        Else
            ReadArray(FilterComboBox.Text)
        End If

    End Sub


    Private Sub ProductListBox_Click() Handles ProductListBox.Click
        'Me.Text = DisplayLOC(ProductListBox.SelectedItem.ToString())
        Try
            If ProductListBox.SelectedItem.ToString() <> Nothing Then
                Label3.Text = ($" You will find {ProductListBox.SelectedItem.ToString()} on aisle") '{DisplayLOC(SearchTextBox.Text)} ")

            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub ResetButton_Click(sender As Object, e As EventArgs) Handles ResetButton.Click
        Defaults()
    End Sub


End Class
' Filter Box can be typed in, it show not be able to
' If "nothing" is clicked in the prodcts list box then it crashes
' Aisle 2 had a blank sopt



'Resolved
' How do i Open a fileDialog?? previous methods are not working.
' Aisle 0-9 not working
'   Aisle 0-9 not working becasue of the padding that alows the sorting to work.