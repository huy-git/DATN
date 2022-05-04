jssor_slider1_init = function () {
  var nestedSliders = [];
  var nestedContainerIds = [
    "sliderh1_container",
    "sliderh2_container",
    "sliderh3_container",
  ];
  for (var i = 0; i < nestedContainerIds.length; i++) {
    containerId = nestedContainerIds[i];
    var nestedSliderOptions = {
      $PauseOnHover: 1,
      $SlideDuration: 500,
      $MinDragOffsetToSlide: 20,
      $SlideSpacing: 3,
      $UISearchMode: 0,
      $BulletNavigatorOptions: {
        $Class: $JssorBulletNavigator$,
        $ChanceToShow: 2,
        $Steps: 1,
        $Rows: 1,
        $SpacingX: 10,
        $SpacingY: 0,
        $Orientation: 1,
      },
    };
    nestedSliders.push(new $JssorSlider$(containerId, nestedSliderOptions));
  }
  var options = {
    $AutoPlay: 0,
    $AutoPlaySteps: 1,
    $Idle: 2000,
    $PauseOnHover: 1,
    $ArrowKeyNavigation: 1,
    $SlideDuration: 300,
    $MinDragOffsetToSlide: 80,
    $SlideSpacing: 3,
    $UISearchMode: 0,
    $PlayOrientation: 2,
    $DragOrientation: 0,
    $ThumbnailNavigatorOptions: {
      $Class: $JssorThumbnailNavigator$,
      $ChanceToShow: 2,
      $ActionMode: 1,
      $Rows: 1,
      $SpacingX: 0,
      $SpacingY: 0,
      $Align: 0,
      $Orientation: 1,
      $NoDrag: false,
    },
  };
  var jssor_slider1 = new $JssorSlider$("slider1_container", options);
  function OnMainSliderPark(currentIndex, fromIndex) {
    for (var i = 0; i < nestedSliders.length; i++) {
      var nestedSlider = nestedSliders[i];
      nestedSlider.$Pause();
    }
    setTimeout(function () {
      nestedSliders[currentIndex].$Play();
    }, 2000);
  }
  jssor_slider1.$On($JssorSlider$.$EVT_PARK, OnMainSliderPark);
  OnMainSliderPark(0, 0);
  function ScaleSlider() {
    var parentWidth = jssor_slider1.$Elmt.parentNode.clientWidth;
    if (parentWidth) jssor_slider1.$ScaleWidth(Math.min(parentWidth, 600));
    else $Jssor$.$Delay(ScaleSlider, 30);
  }
  ScaleSlider();
  $Jssor$.$AddEvent(window, "load", ScaleSlider);
  $Jssor$.$AddEvent(window, "resize", ScaleSlider);
  $Jssor$.$AddEvent(window, "orientationchange", ScaleSlider);
};

