var uservue = new Vue({
	el: '#uservue',
	data:
	{
		orders: [],
		CaseItems: []
	},
	methods:
	{
		loadMyCase: function () {
			var vue = this;
			$.ajax({
				url: '/Item/GetUserCases',
				type: 'GET',
				data: dataPost,
				success: function (response) {
					vue.CaseItems = response.Case;
				},
				error: function () { }
			});
		},
		loadMyOrders: function (id) {
			var vue = this;
			dataPost = {
				userId: id,
			};
			$.ajax({
				url: '/Order/ShowMyOrders',
				type: 'POST',
				data: dataPost,
				success: function (response)
				{
					vue.orders = response.Orders;
				},
				error: function () {}
			});
		}
	}
});