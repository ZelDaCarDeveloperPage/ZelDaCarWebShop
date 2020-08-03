var adminPanelVue = new Vue({
	el: '#adminPanelVue',
	data:
	{
		isActiveRequests: true,
		userRequests: []
	},
	methods:
	{
		ShowRequestList: function () {
			var vue = this;
			dataPost = {
				IsActive: vue.isActiveRequests
			};
			$.ajax({
				type: 'POST',
				data: dataPost,
				url: "/User/GetActivePartRequests",
				success: function (response) {
					vue.userRequests = response.UserRequests;
					console.log(response);
				}
			});
		},
		CompleteUserRequest: function (index)
		{
			var vue = this;
			window.location.href = "/User/CompletePartRequest?userid=" + vue.userRequests[index].UserId;
		}
	}
});