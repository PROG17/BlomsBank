// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var displayActualSum = function () {
  $.get(`/bank/GetActualSumByAccountId?id=${$(this).val()}`, (data) => {
    var sumTag = $('#actual-sum')
    var sumInput = $('#input-actual-sum')
    var sumValTag = $('#actual-sum-validation')
    if (data.isSuccess) {
      sumTag.text(data.text)
      sumInput.val(data.text)
      sumValTag.text('')
    }
    else {
      sumValTag.text(data.text)
    }
  })
}
$('input[name=AccountId]').blur(displayActualSum)
