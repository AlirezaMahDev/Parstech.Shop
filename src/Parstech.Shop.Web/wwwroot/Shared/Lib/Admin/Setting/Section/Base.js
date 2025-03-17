var ErrorData = document.getElementById("Error");

var Section_SectionTypeId = document.getElementById("Section_SectionTypeId");
var Section_SectionName = document.getElementById("Section_SectionName");
var Section_Sort = document.getElementById("Section_Sort");
var Section_CateguryId = document.getElementById("Section_CateguryId");
var Section_ProductId = document.getElementById("Section_ProductId");


var SectionDetail_Caption = document.getElementById("SectionDetail_Caption");
var SectionDetail_SubCaption = document.getElementById("SectionDetail_SubCaption");
var SectionDetail_Link = document.getElementById("SectionDetail_Link");
var SectionDetail_ImageFile = document.getElementById("SectionDetail_ImageFile");
var SectionDetail_Image = document.getElementById("SectionDetail_Image");
var SectionDetail_Alt = document.getElementById("SectionDetail_Alt");
var SectionDetail_CateguryId = document.getElementById("SectionDetail_CateguryId");
var SectionDetail_SectionTypeId = document.getElementById("SectionDetail_SectionTypeId");
var SectionDetail_SectionId = document.getElementById("SectionDetail_SectionId");
var SectionDetail_SlideNavName = document.getElementById("SectionDetail_SlideNavName");
var SectionDetail_ResponsiveSize = document.getElementById("SectionDetail_ResponsiveSize");
var SectionDetail_BackgroundColor = document.getElementById("SectionDetail_BackgroundColor");
var SectionDetail_ColSpace = document.getElementById("SectionDetail_ColSpace");

var image = document.getElementById("image");


function SectionIdForIllInput(id) {
    var IdList = document.querySelectorAll('.Section_Id');
    IdList.forEach(function (input) {
        input.value = id;
    });


}

function SectionDetailIdForIllInput(id) {
    var DetailIdList = document.querySelectorAll('.SectionDetail_Id');
    DetailIdList.forEach(function (input) {
        input.value = id;
    });
}

function CleanModal() {
    SectionIdForIllInput(0);
    SectionDetailIdForIllInput(0);

    Section_SectionTypeId.value = null;
    Section_SectionName.value = null;
    Section_Sort.value = null;
    Section_ProductId.value = null;
    Section_CateguryId.value = null;
    SectionDetail_BackgroundColor.value = null;


    SectionDetail_SlideNavName.value = null;
    SectionDetail_Caption.value = null;
    SectionDetail_SubCaption.value = null;
    SectionDetail_Image.value = null;
    SectionDetail_Link.value = null;
    SectionDetail_CateguryId.value = null;
    SectionDetail_Alt.value = null;
    SectionDetail_SectionTypeId.value = null;
    SectionDetail_ResponsiveSize.value = null;
    image.innerHTML = null;

}


function SectionCreate() {
    CleanModal();
    $('#CreateSectionModal').modal('show');

}

function SectionDetailCreate(id) {
    CleanModal();
    SectionIdForIllInput(id);
    $('#CreateSectionDetailModal').modal('show');

}

function SectionDelete(id) {
    CleanModal();
    SectionIdForIllInput(id);
    $('#DeleteSectionModal').modal('show');

}

function SectionDetailDelete(id) {
    CleanModal();
    SectionDetailIdForIllInput(id);
    $('#DeleteSectionDetailModal').modal('show');

}






