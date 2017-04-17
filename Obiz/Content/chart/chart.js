/**
 * @namespace Chart
 */
var Chart = require('~/Content/core/core.js')();

require('~/Content/core/core.helpers')(Chart);
require('~/Content/platforms/platform.js')(Chart);
require('~/Content/core/core.canvasHelpers')(Chart);
require('~/Content/core/core.element')(Chart);
require('~/Content/core/core.plugin.js')(Chart);
require('~/Content/core/core.animation')(Chart);
require('~/Content/core/core.controller')(Chart);
require('~/Content/core/core.datasetController')(Chart);
require('~/Content/core/core.layoutService')(Chart);
require('~/Content/core/core.scaleService')(Chart);
require('~/Content/core/core.ticks.js')(Chart);
require('~/Content/core/core.scale')(Chart);
require('~/Content/core/core.title')(Chart);
require('~/Content/core/core.legend')(Chart);
require('~/Content/core/core.interaction')(Chart);
require('~/Content/core/core.tooltip')(Chart);

require('~/Content/elements/element.arc')(Chart);
require('~/Content/elements/element.line')(Chart);
require('~/Content/elements/element.point')(Chart);
require('~/Content/elements/element.rectangle')(Chart);

require('~/Content/scales/scale.linearbase.js')(Chart);
require('~/Content/scales/scale.category')(Chart);
require('~/Content/scales/scale.linear')(Chart);
require('~/Content/scales/scale.logarithmic')(Chart);
require('~/Content/scales/scale.radialLinear')(Chart);
require('~/Content/scales/scale.time')(Chart);

// Controllers must be loaded after elements
// See Chart.core.datasetController.dataElementType
require('~/Content/controllers/controller.bar')(Chart);
require('~/Content/controllers/controller.bubble')(Chart);
require('~/Content/controllers/controller.doughnut')(Chart);
require('~/Content/controllers/controller.line')(Chart);
require('~/Content/controllers/controller.polarArea')(Chart);
require('~/Content/controllers/controller.radar')(Chart);

require('~/Content/charts/Chart.Bar')(Chart);
require('~/Content/charts/Chart.Bubble')(Chart);
require('~/Content/charts/Chart.Doughnut')(Chart);
require('~/Content/charts/Chart.Line')(Chart);
require('~/Content/charts/Chart.PolarArea')(Chart);
require('~/Content/charts/Chart.Radar')(Chart);
require('~/Content/charts/Chart.Scatter')(Chart);

window.Chart = module.exports = Chart;
