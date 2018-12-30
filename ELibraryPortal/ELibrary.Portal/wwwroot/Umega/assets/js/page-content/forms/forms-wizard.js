$(document).ready(function() {
	$('#form-horizontal').steps({
		headerTag: 'h3',
		bodyTag: 'fieldset',
        transitionEffect: 'slide',
         labels: {
            current: "Mevcut Ad�m",
            finish: "Kaydet",
            next: "Sonraki",
            previous: "�nceki",
            loading: "Y�kleniyor ..."
        }
	});
	$('#form-vertical').steps({
		headerTag: 'h3',
		bodyTag: 'fieldset',
		transitionEffect: 'slide',
		stepsOrientation: 'vertical'
	});
	$('#form-tabs').steps({
		headerTag: 'h3',
		bodyTag: 'fieldset',
		transitionEffect: 'slideLeft',
		enableFinishButton: false,
		enablePagination: false,
		enableAllSteps: true,
		titleTemplate: '#title#',
		cssClass: 'tabcontrol'
	});
});