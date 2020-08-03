var ordervue = new Vue({
	el: '#homevue',
	data:
	{
		VINInput: false,
		CarInfoLoaded: false,
		CurrentArtID: '',
		SearchInput: '', // поисковая строка
		VinCode: '',	// вин код
		CarModel: '',	// модель автомобиля
		CarPower: '',	// Мощность 
		Currentbrand: '', // Искать по бренду
		CarDatailIds: [], // Номер каталога для поиска
		// Параметр используем для выбора LoadPartsIds
		// При вызове метода loadPartnerLists
		SearchPartInput: '',
		LoadPartIds: [],  // id ориг. частей машины
		// флаги состояния для выбора каталогов
		Partneritems: [],
		isAllRad: true,
		isRad1TurnOn: false, // 
		isRad2TurnOn: false, //
		isRad3TurnOn: false, //
		isRad4TurnOn: false, // 
		isRad5TurnOn: false, //
		isRad6TurnOn: false, //
		isRad7TurnOn: false, //
		isRad8TurnOn: false, //
		isRad9TurnOn: false, //
		isRad10TurnOn: false, //
		isRad11TurnOn: false//
	},
	methods:
	{
		SetRadios: function (actRad) {
			var vue = this;
			vue.isRad11TurnOn = false;
			vue.isRad10TurnOn = false;
			vue.isRad9TurnOn = false;
			vue.isRad8TurnOn = false;
			vue.isRad7TurnOn = false;
			vue.isRad6TurnOn = false;
			vue.isRad5TurnOn = false;
			vue.isRad4TurnOn = false;
			vue.isRad3TurnOn = false;
			vue.isRad2TurnOn = false;
			vue.isRad1TurnOn = false;
			vue.isAllRad = false;
			switch (actRad)
			{
				case 1:
					// Search by vin
					vue.isAllRad = true;
					break;
				case 2:
					// Open panel 1
					vue.isRad1TurnOn = true;
					break;
				case 3:
					// Open panel 2
					vue.isRad2TurnOn = true;
					break;
				case 4:
					// Open panel 3
					vue.isRad3TurnOn = true;
					break;
				case 5:
					// Open panel 4
					vue.isRad4TurnOn = true;
					break;
				case 6:
					// Open panel 5
					vue.isRad5TurnOn = true;
					break;
				case 7:
					// Open panel 6
					vue.isRad6TurnOn = true;
					break;
				case 8:
					// Open panel 7
					vue.isRad7TurnOn = true;
					break;
				case 9:
					// Open panel 8
					vue.isRad8TurnOn = true;
					break;
				case 10:
					// Open panel 9
					vue.isRad9TurnOn = true;
					break;
				case 11:
					// Open panel 10
					vue.isRad10TurnOn = true;
					break;
				case 12:
					// Open panel 11
					vue.isRad11TurnOn = true;
					break;
			}
		},
		loadOrig: function (index) {
			var self = this;
			self.loadPartnerLists(index, true);
		},
		loadNonOrig: function (index) {
			var self = this;
			self.loadPartnerLists(index, false);
		},
		loadPartnerLists: function (index, isOrig) {
			var vue = this;
			if (vue.filteredList == null) return;
			var gid = vue.filteredList[index].Value;
			dataPost = {
				id: gid,
				brand: vue.Currentbrand,
				IsByVin: true,
				IsOnlyOriginal: isOrig
			};
			$.ajax({
				url: '/Item/GetListParam',
				type: 'POST',
				data: dataPost,
				success: (response) => {
					vue.Partneritems = response.PartnerItemList;
				}
			});
		},
		findPartnerParts: function () {
			var vue = this;
		},
		loadOrigByCode: function () {
			var vue = this;
			vue.loadPartnerListsByCode(true);
		},
		loadNonOrigByCode: function () {
			var vue = this;
			vue.loadPartnerListsByCode(false);
		},
		loadPartnerListsByCode: function (isOrig) {
			var vue = this;
			if (vue.CurrentArtID == null || vue.CurrentArtID.length == 0)
			{
				alert('Введите номер запчасти');
				return;
			}
			dataPost = {
				id: vue.CurrentArtID,
				brand: vue.Currentbrand,
				IsByVin: false,
				IsOnlyOriginal: isOrig
			};
			$.ajax({
				url: '/Item/GetListParam',
				type: 'POST',
				data: dataPost,
				success: (response) => {
					console.log(response);
					vue.Partneritems = response.PartnerItemList;
				},
				error: () => {
					console.log("Error");
				},
				complete: () => {
					console.log("Complete");
				}
			});
		},
		highlight: function (id) {
			var IdElem = 'btnid' + id;
			console.log(IdElem);
			var element = document.getElementById(IdElem);
			element.style.color = "green";
		},
		addCaseItem: function (indexOf)
		{
			var vue = this;
			vue.highlight(indexOf);
			var curItem = vue.Partneritems[indexOf];
			console.log(curItem);
			dataPost = {
				PRICE: curItem.PRICE,
				COUNT: curItem.COUNT,
				MANUFACTURER: curItem.MANUFACTURER,
				EndDate: curItem.EndDate,
				NAME: curItem.NAME,
				GID: curItem.GID
			};
			$.ajax({
				url: '/Item/AddUserCase',
				type: 'POST',
				data: dataPost,
				success: function () {
				}
			});
		},
		UpdatePartsList: function () {
			var vue = this;
			dataPost = {};
			$.ajax({
				url: '/User/GetPartsList',
				type: 'POST',
				data: dataPost,
				success: function (response) {
					vue.LoadPartIds = response.Arts;
					alert(response.Message);
				},
				error: function (data) {
					alert('Ошибка');
				}
			})
		},
		LoadCarInfo: function ()
		{
			if (this.VinCode.length == 0) {
				alert('Введите вин-код');
				return;
			}
			var vue = this;
			console.log(vue.VinCode);
			dataPost = {
				vin: vue.VinCode,
			};
			$.ajax({
				url: '/Home/GetAutoModel',
				type: 'POST',
				data: dataPost,
				success: function (response) {
					if (response.State == 0) {
						vue.CarModel = response.CarModel;
						vue.CarInfoLoaded = true;
						vue.CarPower = response.CarPower;
						vue.CarDatailIds = response.carIds;
					}
					else
					{
						$('.alert').alert();
					}
				},
				error: function (data) {
					alert('Ошибка');
				}
			})
		}
	},
	created: function () {
		var vue = this;
		vue.UpdatePartsList();
	},
	computed:
	{
		filteredList() {
			var vue = this;
			return vue.LoadPartIds.filter(post => {
				if (vue.SearchInput.length >= 1) {
					return post.Title.toLowerCase().includes(vue.SearchInput.toLowerCase())
				}
				else
				{
					return null;
				}
			}
			)
		}
	}
});