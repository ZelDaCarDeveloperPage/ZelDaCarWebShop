var UserCaseVue = new Vue({
    el: '#casevue',
    data: {
        UserOrderMessage: ''
    },
    methods:
    {
        PayCase: function (userId, gid, Partner)
        {
            console.log(userId + '-' + gid + '-' + Partner)
            var vue = this;
            dataPost = {
                UserMessage: vue.UserOrderMessage,
                UserId: userId,
                GID: gid,
                Partner: Partner
            };
            $.ajax({
                data: dataPost,
                url: "/Order/PayOrder",
                success: function (response)
                {

                }
                    
            });
        } 
    }


});