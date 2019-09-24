// JavaScript source code
$(document).ready(function () {

    loadItems();
    $("#message").val("");

    $("#makePurchase").click(function (event) {

        $("#message").val("");
        $("#errorMessages").empty();
        $("#change").val("");
        var item = $("#itemSelect").val();

        if (item == "") {
            $("#errorMessages")
                .append($("<li>")
                    .attr({ class: "list-group-item list-group-item-danger" })
                    .text("Error! Must select an item for purchase!."));
        }
        else {

            makePurchase();
        }
        
    })

    $("#addDollar").click(function (event) {
        var money = $("#moneyIn").val();
        var newMoney = Number(money) + 1.00;
        $("#moneyIn").val(newMoney.toFixed(2));

        $("#changeReturnDiv").show();
        $("#message").val("");
        $("#change").val("");
    })

    $("#addQuarter").click(function (event) {
        var money = $("#moneyIn").val();
        var newMoney = Number(money) + 0.25;
        $("#moneyIn").val(newMoney.toFixed(2));

        $("#changeReturnDiv").show();
        $("#message").val("");
        $("#change").val("");
    })

    $("#addDime").click(function (event) {
        var money = $("#moneyIn").val();
        var newMoney = Number(money) + 0.10;
        $("#moneyIn").val(newMoney.toFixed(2));

        $("#changeReturnDiv").show();
        $("#message").val("");
        $("#change").val("");
    })

    $("#addNickel").click(function (event) {
        var money = $("#moneyIn").val();
        var newMoney = Number(money) + 0.05;
        $("#moneyIn").val(newMoney.toFixed(2));

        $("#changeReturnDiv").show();
        $("#message").val("");
        $("#change").val("");
    })

    $("#changeReturn").click(function (event) {
        makeChange();
    })
});

function createItemDivs(itemArray) {
    $.each(itemArray, function (index, item) {
        if (index <= 2) {
            var itemRow = $("#itemsOneToThree");
        }
        else if (index > 2 && index < 6) {
            var itemRow = $("#itemsFourToSix");
        }
        else {
            var itemRow = $("#itemsSevenToNine");
        }

        var id = item.id;
        var name = item.name;
        var price = item.price;
        var quantity = item.quantity;

        var itemDiv = "<a style='text-decoration: none; color:inherit' onclick='itemSelect(" + id + ")'><div class='col-md-2' style='border: solid; margin: 25px 30px 25px 30px'>";
        itemDiv += "<p>" + id + "</p>";
        itemDiv += "<p class='text-center'>" + name + "</p>";
        itemDiv += "<p class='text-center'>$" + price + "</p>";
        itemDiv += "<p class='text-center'>Quantity Left:" + quantity + "</p>";
        itemDiv += "</div></a>";

        itemRow.append(itemDiv);
    })
}

function errorMessage() {
    $("#errorMessages")
        .append($("<li>")
            .attr({ class: "list-group-item list-group-item-danger" })
            .text("Error calling web service. Please try again later."));
}

function loadItems() {
    clearItemDivs();
    $("#errorMessages").empty();

    $.ajax({
        type: "GET",
        url: "http://localhost:8080/items",
        success: function (itemArray) {
            createItemDivs(itemArray);
        },
        error: function () {
            errorMessage();
        }
    })
}

function makePurchase() {
    $("#message").val("");
    var money = $("#moneyIn").val();
    var item = $("#itemSelect").val();

    if (money == "") {
        money = 0;
    }

    $.ajax({
        type: "GET",
        url: "http://localhost:8080/money/" + money + "/item/" + item,
        success: function (response) {
            var change = "";
            if (response.quarters > 0) {
                if (response.quarters == 1) {
                    change += response.quarters + " Quarter ";
                }
                else {
                    change += response.quarters + " Quarters ";
                }
            }

            if (response.dimes > 0) {
                if (response.dimes == 1) {
                    change += response.dimes + " Dime ";
                }
                else {
                    change += response.dimes + " Dimes ";
                }
            }

            if (response.nickels > 0) {
                if (response.nickels == 1) {
                    change += response.nickels + " Nickel ";
                }
                else {
                    change += response.nickels + " Nickels ";
                }
            }

            if (response.pennies > 0) {
                if (response.pennies == 1) {
                    change += response.pennies + " Penny";
                }
                else {
                    change += response.pennies + " Pennies";
                }
            }

            $("#message").val("THANK YOU!!");
            $("#change").val(change)
            $("#moneyIn").val("");
            $("#changeReturnDiv").hide();
        },
        error: function (errorMessage) {

            $("#message").val(errorMessage.responseJSON.message);
        }
    })

    
    $("#itemSelect").val("");
    loadItems();
    
}

function makeChange() {
    var moneyIn = $("#moneyIn").val();
    var moneyReturned = moneyIn;
    var change = "";

    var quarters = ~~(moneyReturned / 0.25);
    var dimes = (Math.ceil((moneyReturned % 0.25) * 100) / 100)/ 0.1;
    var nickels = ((Math.ceil((moneyReturned % 0.25) * 100) /100) % 0.1) / 0.05;


    if (quarters >= 1) {
        if (quarters == 1) {
            change += quarters + " Quarter ";
        }
        else {
            change += quarters + " Quarters ";
        }
    }

    if (dimes >= 1) {
        if (dimes == 1) {
            change += dimes + " Dime ";
        }
        else {
            change += dimes + " Dimes ";
        }
    }

    if (nickels >= 1) {
        if (nickels == 1) {
            change += nickels + " Nickel ";
        }
        else {
            change += nickels + " Nickels ";
        }
    }


    $("#moneyIn").val("");
    $("#message").val("CHANGE RETURNED");
    $("#change").val(change);
    $("#changeReturnDiv").hide();




}

function clearItemDivs() {
    $("#itemsOneToThree").empty();
    $("#itemsFourToSix").empty();
    $("#itemsSevenToNine").empty();
}

function itemSelect(id) {
    $("#itemSelect").val(id);
}