var partsRequestVue = new Vue({
    el: '#partsRequestVue',
    data: {
        vinCode: '',
        currentParts: [],
        currentTitle: '',
        Parts: [],
        filteredParts: []
    },
    methods: {
        GetShopItemList: function () {
            var vue = this;
            dataPost = {};
            $.ajax({
                url: '/User/GetShopItems',
                type: 'POST',
                data: dataPost,
                success: (response) => {
                    vue.Parts = response.Items;
                    alert('В нашем магазине доступно ' + vue.Parts.length + ' различных наименований запчастей. Начните вводить название');
                },
            });
        },
        SelectPart: function (index) {
            var vue = this;
            vue.currentTitle = vue.filteredList[index];
        },
        AddNewPart: function ()
        {
            var vue = this;
            vue.currentParts.push(vue.currentTitle);
            vue.currentTitle = '';
        },
        SendRequest: function ()
        {
            var vue = this;
            dataPost = {
                VinCode: vue.vinCode,
                Parts: vue.currentParts
            };
            $.ajax({
                url: '/User/SendPartsRequestForArts',
                type: 'POST',
                data: dataPost,
                success: (response) => {
                    alert(response.Message);
                },
                error: () => {
                    alert('Произошла ошибка. Попробуйте позже');
                },
            });
        }
    },
    created: function () {
        var vue = this;
        vue.GetShopItemList();
    },
    computed:
    {
        filteredList() {
            var vue = this;
            return vue.Parts.filter(post => {
                if (vue.currentTitle.length >= 1) {
                    return post.toLowerCase().includes(vue.currentTitle.toLowerCase())
                }
                else return null;
            })
        }
    }
});