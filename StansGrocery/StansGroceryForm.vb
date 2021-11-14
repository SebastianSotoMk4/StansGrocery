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

    'sets defaults when loaded or called
    Sub Defaults()
        CategoryRadioButton.Checked = True
    End Sub



    'keep
    Private Sub SearchButton_Click() Handles SearchButton.Click
        If SearchTextBox.Text = "zzz" Then
            Me.Close()
        Else
            ReadArray(SearchTextBox.Text)
        End If
    End Sub
    Sub ReadArray(searchWord As String)
        Dim tempItem() As String
        Dim jankRecord As String

        ProductListBox.Items.Clear()

        For i = LBound(Me.groceryData) To UBound(Me.groceryData)
            jankRecord = ($"{groceryData(i, 0)} {groceryData(i, 1)} {groceryData(i, 2)} ")

            tempItem = Split(groceryData(i, 0), "$$ITM")
            Try
                Console.WriteLine(tempItem(1))
                If InStr(jankRecord, searchWord, CompareMethod.Text) > 0 Then
                    ProductListBox.Items.Add($"{tempItem(1)}")
                End If
            Catch ex As Exception
                Console.WriteLine("oops")
            End Try
        Next
    End Sub
    Sub ReadFile()
        'Clear listbox each time called for neatness
        ProductListBox.Items.Clear()
        Dim currentLine As String
        Dim temp() As String
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
        For i = LBound(groceryData) To UBound(groceryData)
            Try
                temp = Split(groceryData(i, index), delimiter)
                'FilterComboBox.Items.Add(temp)
                If FilterComboBox.Items.Contains(temp(1).PadLeft(2)) = False And temp(1) <> "" Then
                    FilterComboBox.Items.Add(temp(1).PadLeft(2))

                End If
            Catch ex As Exception

            End Try
        Next
        FilterComboBox.Sorted = True
    End Sub
    Private Sub FilterComboBox_SelectedIndexChanged() Handles FilterComboBox.SelectedIndexChanged
        ReadArray(FilterComboBox.Text)
    End Sub

    Private Sub ProductListBox_Click() Handles ProductListBox.Click

    End Sub
End Class
' How do i Open a fileDialog?? previous methods are not working.


'    'Function SeperateRecords(ByVal record As String) As String
'    Dim temp() As String
'    Dim readLable() As String
'    temp = Split(record, ",")

'    For i = UBound(groceryData) To LBound(groceryData)
'        Select Case groceryData(0, i)
'            Case groceryData(0, i)
'                readLable = Split(temp(0), "$$")
'            Case groceryData(1, i)
'                readLable = Split(temp(1), "##")
'            Case groceryData(2, i)
'                readLable = Split(temp(2), "%%")
'        End Select


'    Next
'End Function

'Sub SeperateGrocerys()
'    Dim temp() As String
'    Dim readLable() As String

'    For i = UBound(Me.groceryData) To LBound(Me.groceryData)
'        readLable = Split(temp(0), "$$")
'        'Me.cleanGroceryData(0, i) = readLable
'    Next

'End Sub


'keep
'Sub DisplayFile()
'    Dim writeLine As String
'    ProductListBox.Items.Clear()
'    For i = LBound(groceryData) To UBound(groceryData)
'        writeLine = groceryData(i, 0) + groceryData(i, 1) + groceryData(i, 2)
'        ProductListBox.Items.Add($"{writeLine}")
'    Next
'End Sub
'Sub NewReadFile()
'    Dim currentLine As String
'    Dim itemNo As Integer
'    Try
'        FileOpen(1, Me.fileName, OpenMode.Input)
'        Do Until EOF(1)
'            Input(1, currentLine)
'            tempList.Add($"{currentLine}")
'        Loop
'        FileClose(1)
'    Catch ex As Exception
'    End Try
'End Sub