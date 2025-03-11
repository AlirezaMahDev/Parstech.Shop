function SectionGetSubmit(id) {
    SectionIdForIllInput(id);
    $("#SectionForm").submit();
    
}

function SectionDetailGetSubmit(id) {
   
    SectionDetailIdForIllInput(id);
    $("#SectionDetailForm").submit();
   
}



//section
function OnLoadingSection() {
   // console.log("Loading");
}

function OnCompleteSection(xhr) {
   //console.log(xhr.responseJSON);
    response = xhr.responseJSON.object;

    $('#CreateSectionModal').modal('show');
    Section_SectionTypeId.value = response.sectionTypeId;
    Section_SectionName.value = response.sectionName;
    Section_Sort.value = response.sort;
    Section_ProductId.value = response.productId;
    Section_CateguryId.value = response.categuryId;
    

    
}
function OnErrorSection() {

}

//sectionDetail
function OnLoadingSectionDetail() {
  //  console.log("Loading");
    image.innerHTML = null;
}



function OnCompleteSectionDetail(xhr) {
    response = xhr.responseJSON.object;
    $('#CreateSectionDetailModal').modal('show');
  //  console.log(SectionDetail_Caption.value);
  //  console.log(response.sectionTypeId);


    SectionDetail_Caption.value = response.caption;
    SectionDetail_SlideNavName.value = response.slideNavName;
    SectionDetail_SubCaption.value = response.subCaption;
    SectionDetail_Image.value = response.image;
    SectionDetail_Link.value = response.link;
    SectionDetail_BackgroundImage.value = response.backgroundImage;
    if (response.backgroundImage != null) {
        image.insertAdjacentHTML("afterbegin", "<img width='100%' src='/Shared/Images/" + response.backgroundImage + "'/><br/>");
    }
    if (response.image != null) {
        image.insertAdjacentHTML("beforeend", "<img width='170' src='/Shared/Images/" + response.image + "'/>");
    }
   
    
    SectionDetail_Alt.value = response.alt;
    SectionDetail_SectionTypeId.value = response.sectionTypeId;
    SectionDetail_SectionId.value = response.sectionId;
    SectionDetail_ResponsiveSize.value = response.responsiveSize;
    SectionDetail_BackgroundColor.value = response.backgroundColor;
    SectionDetail_ColSpace.value = response.colSpace;
}
function OnErrorSectionDetail() {
    $('#modal').modal('show');
}
