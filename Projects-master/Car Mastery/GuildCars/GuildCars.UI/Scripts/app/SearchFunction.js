function searchResults(array, button) {
    $.each(array, function (index, listing) {
        var html = '<div class = "row searchResult">' +
            '<div class="row">' +
            '<div class="col-xs-12">' +
            '<p><strong>' + listing.Year + ' ' + listing.VehicleMakeName + ' ' + listing.VehicleModelName + '</strong></p>' +
            '</div>' +
            '</div>' +
            '<div class="row">' +
            '<div class="col-xs-12 col-sm-12 col-md-3">' +
            '<img src="' + imagePath + listing.ImageFileName + '" />' +
            '</div>' +
            '<div class="col-xs-12 col-sm-12 col-md-3">' +
            '<table>' +
            '<tbody>' +
            '<tr>' +
            '<td><strong>Body Style:</strong></td>' +
            '<td>' + listing.BodyStyleName + '</td>' +
            '</tr>' +
            '<tr>' +
            '<td><strong>Trans:</strong></td>' +
            '<td>' + listing.TransmissionTypeName + '</td>' +
            '</tr>' +
            '<tr>' +
            '<td><strong>Color:</strong></td>' +
            '<td>' + listing.Color + '</td>' +
            '</tr>' +
            '</tbody>' +
            '</table>' +
            '</div>' +
            '<div class="col-xs-12 col-sm-12 col-md-3">' +
            '<table>' +
            '<tbody>' +
            '<tr>' +
            '<td><strong>Interior:</strong></td>' +
            '<td>' + listing.InteriorColor + '</td>' +
            '</tr>' +
            '<tr>' +
            '<td><strong>Mileage:</strong></td>' +
            '<td>' + listing.Mileage + '</td>' +
            '</tr>' +
            '<tr>' +
            '<td><strong>VIN #:</strong></td>' +
            '<td>' + listing.VIN + '</td>' +
            '</tr>' +
            '</tbody>' +
            '</table>' +
            '</div>' +
            '<div class="col-xs-12 col-sm-12 col-md-3">' +
            '<table>' +
            '<tbody>' +
            '<tr>' +
            '<td><strong>Sale Price:</strong></td>' +
            '<td>' + listing.SalePrice + '</td>' +
            '</tr>' +
            '<tr>' +
            '<td><strong>MSRP:</strong></td>' +
            '<td>' + listing.MSRP + '</td>' +
            '</tr>' +
            '</tbody>' +
            '</table>' +
            '</div>' +
            '</div>' +
            '<div class="row">' +
            '<div class="col-xs-offset-9 col-xs-3">' +
            '<p>' +
            '<button onclick="location.href = ' + "'" + detailsPath + listing.VehicleListingId + "'" + ';">' + button + '</button>'
        '</p>' +
            '</div>' +
            '</div>' +
            '</div>';


        $('#searchResults').append(html.toString());
    });
}