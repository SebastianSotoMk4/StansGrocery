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
    Sub SampleTxt()
        Dim currentRecord As String

        FileOpen(1, "Grocery.txt", OpenMode.Input)
        Do Until EOF(1)
            Input(1, currentRecord)
            ProductListBox.Text = currentRecord
            'Console.WriteLine(currentRecord)

        Loop

        FileClose(1)



    End Sub

    Sub Defaults()
        AisleRadioButton.Checked = True

    End Sub
    Sub ReadFile()
        'Clear listbox each time called for neatness
        ProductListBox.Items.Clear()

        Dim currentRecord As String
        Dim temp() As String

        FileOpen(1, Me.fileName, OpenMode.Input)

        Do Until EOF(1)
            Input(1, currentRecord)
            ProductListBox.Items.Add($"{currentRecord}")

        Loop
        FileClose(1)
    End Sub

    Function SeperateRecords(ByVal record As String) As String

    End Function

    Private Sub SearchButton_Click(sender As Object, e As EventArgs) Handles SearchButton.Click
        ReadFile()
    End Sub
End Class
''Could not find file 'C:\Users\thefi\source\repos\StansGrocery\StansGrocery\.Grocery.txt'.'