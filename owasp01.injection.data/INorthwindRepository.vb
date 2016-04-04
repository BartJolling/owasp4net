Public Interface INorthWindRepository
    Function LoadProducts(CategoryId As String) As IEnumerable(Of Product)
End Interface
