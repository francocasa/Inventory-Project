sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/m/MessageToast",
    "sap/ui/model/json/JSONModel",
], function (Controller, MessageToast, JSONModel) {
    "use strict";
    return Controller.extend("MyApp.App", {
        onInit: function () {
            console.log("init") ;
        },
        onAfterRendering: function () {
            var self = this;
            console.log("after rendering");
            var oTable = self.getView().byId("usersTable");
            var oModel = new JSONModel();
			oModel.setDefaultBindingMode(sap.ui.model.BindingMode.TwoWay);
			oModel.attachRequestSent(function () {
                //console.log("request sent");
            });
			oModel.attachRequestCompleted(function (oControlEvent) {
                //console.log("request completed");
                var data = oModel.getData();
                console.log(data);
                var sType = "Active";
                var oTemplate = new sap.m.ColumnListItem({
                    type: sType,
                    cells: [
                        new sap.m.Text({
                            text: "{users>userId}"
                        }),
                        new sap.m.Text({
                            text: "{users>userName}"
                        })
                    ]
                });
                self.getView().setModel(oModel, "users");
                self.getView().bindElement("/");
                //oTable.setModel(oModel, "users");
                oTable.bindItems("users>/", oTemplate);
			});
			oModel.attachRequestFailed(function (oControlEvent) {
				//console.log("request failed");
            });
            //loadData(sURL, oParameters?, bAsync?, sType?, bMerge?, bCache?, mHeaders?)
			oModel.loadData("http://plngsnias18/ITInventory/api/Users", null, true, "GET", false, false, null);
        }
    });
});