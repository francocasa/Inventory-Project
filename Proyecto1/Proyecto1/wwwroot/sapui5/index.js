sap.ui.getCore().attachInit(function () {
    sap.ui.require(["sap/ui/core/mvc/XMLView"], function(XMLView) {
     XMLView.create({
     viewName: "MyApp.App"}).then(function(myView) {
         myView.placeAt("content");
      });
     });
  });