(function (module) {

    var appinsightsLogger = function ($q) {

        appInsights = appInsights || {};

        var verifyAppInsights = function () {
            if (Object.keys(appInsights).length === 0) {
                return false;
            }
            return true;
        }

        var logConsoleException = function(exception, originalMessage) {
            $log.error("Unable to log error in Application Insights. Error Received - " + exception);
            $log.warn("Original messgae" + originalMessage);
        }

        var logMessage = function (message, sevLevel) {
            try {
                appInsights.trackTrace(message, sevLevel);
            } catch (exception) {
                logConsoleException(exception, message);
            }
        }

        var logPageView = function (pageName, pageUrl) {
            try {
                appInsights.trackPageView(pageName, pageUrl);
            } catch (excption) {
                logConsoleException(exception, pageName);
            }
        }

        var logEvent = function (event, properties, measurements) {
            try {
                appInsights.trackEvent(event, properties, measurements);
            } catch (excption) {
                logConsoleException(exception, event);
            }
        }

        var logMetric = function (metricName, metricValue, properties) {
            try {
                appInsights.trackMetric(metricName, metricValue, properties);
            } catch (exception) {
                logConsoleException(exception, metricName);
            }
        }

        var logException = function (exception) {
            try {
                appInsights.trackException(exception);
            } catch (exception) {
                logConsoleException(exception, metricName);
            }
        }

        return {
            logView: logPageView,
            logMessage: logMessage,
            logEvent: logEvent,
            logMetric: logMetric,
            logException: logException
        };
    }

    module.factory("appInsightsLogger", appinsightsLogger);

}(angular.module("common")))