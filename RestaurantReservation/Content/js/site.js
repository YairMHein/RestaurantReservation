

function overlayLoadingShow() {
    $('#overlayloading').show();
}


function overlayLoadingHide() {
    $('#overlayloading').hide();
}


function listLoadingShow(ID) {
    $('#' + ID).empty().append('<div class="text-center mt-100"><div class="lds-ellipsis"><div></div><div></div><div></div><div></div></div></div>');
}


function listLoadingHide() {
    $('#' + ID).empty();
}



function skeletonListLoadingShow(ID) {

    var elements = '<div class="row mb-7">';
    for (var i = 0; i < 6; i++) {
        elements += `
                    <div class="col-sm-6 col-lg-4 mb-4">
                        <!-- Card -->
                        <div class="card h-100">
                            <div class="shape-container skeleton" style="min-height: 270px; border-top-left-radius: 0.4375rem; border-top-right-radius: 0.4375rem">

                                <!-- Shape -->
                                <div class="shape shape-bottom zi-1" style="margin-bottom: -.25rem">
                                    <svg xmlns="http://www.w3.org/2000/svg" x="0px" y="0px" viewBox="0 0 1920 100.1">
                                        <path fill="#fff" d="M0,0c0,0,934.4,93.4,1920,0v100.1H0L0,0z"></path>
                                    </svg>
                                </div>
                                <!-- End Shape -->
                            </div>

                            <!-- Card Body -->
                            <div class="card-body">
                                <div class="mb-2 skeleton" style="min-height:27px">

                                </div>
                                <h4 class="card-title skeleton" style="min-height:57px">
                                </h4>

                                <p class="card-text skeleton" style="min-height:100px"></p>
                            </div>
                            <!-- End Card Body -->
                            <!-- Card Footer -->
                            <div class="card-footer">
                                <div class="d-flex skeleton" style="min-height:50px">

                                </div>

                            </div>
                            <!-- End Card Footer -->
                        </div>
                        <!-- End Card -->
                    </div>
               `
    }

    elements += `</div>`;


    $('#' + ID).empty().append(elements);
}




function skeletonBlogListByCategoryLoadingShow(ID) {

    var elements = '';
    for (var i = 0; i < 6; i++) {
        elements += `
                    <div class="card mb-5 mb-md-10">
                <div class="shape-container skeleton" style="min-height: 406px; border-top-left-radius: 0.4375rem; border-top-right-radius: 0.4375rem">

                    <!-- Shape -->
                    <div class="shape shape-bottom zi-1" style="margin-bottom: -.25rem">
                        <svg xmlns="http://www.w3.org/2000/svg" x="0px" y="0px" viewBox="0 0 1920 100.1">
                            <path fill="#fff" d="M0,0c0,0,934.4,93.4,1920,0v100.1H0L0,0z"></path>
                        </svg>
                    </div>
                    <!-- End Shape -->
                </div>

                <!-- Card Body -->
                <div class="card-body">
                    <h3 class="card-title skeleton" style="min-height:37px">

                    </h3>

                    <p class="skeleton" style="min-height:81px"></p>
                </div>
                <!-- End Card Body -->
                <!-- Card Footer -->
                <div class="card-footer">
                    <div class="d-flex align-items-center">
                        <div class="flex-shrink-0">
                            <div class="skeleton" style="min-height:50px;min-width:50px;border-radius:50%">

                            </div>
                        </div>

                        <div class="flex-grow-1">
                            <div class="d-flex justify-content-end">
                                <div class="card-link link-dark skeleton" style="min-height:27px;min-width:120px"></div>

                            </div>
                            <div class="d-flex justify-content-end">

                                <p class="card-text small skeleton" style="min-height: 27px; min-width: 120px"></p>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- End Card Footer -->
            </div>
               `
    }



    $('#' + ID).empty().append(elements);
}
