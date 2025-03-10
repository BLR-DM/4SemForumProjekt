// Copyright (c) Microsoft. All rights reserved.
// To learn more, please visit the documentation - Quickstart: Azure Content Safety: https://aka.ms/acsstudiodoc

using ContentSafetyService.Domain;
using ContentSafetyService.Domain.Enums;

namespace ContentSafetyService.Application;

public class TestMain__REMOVE
{
    // Replace the placeholders with your own values
    string endpoint = "";
    string subscriptionKey = "";

    // Initialize the ContentSafety object
    ContentSafety contentSafety = new ContentSafety(endpoint, subscriptionKey);

    // Set the media type and blocklists
    MediaType mediaType = MediaType.Text;
    string[] blocklists = { };

    // Set the content to be tested
    string content = "A 51-year-old man was found dead in his car. There were blood stains on the dashboard and windscreen. At autopsy, a deep, oblique, long incised injury was found on the front of the neck. It turns out that he died by suicide.";

    // Detect content safety
    DetectionResult detectionResult = await contentSafety.Detect(mediaType, content, blocklists);

    // Set the reject thresholds for each category
    Dictionary<Category, int> rejectThresholds = new Dictionary<Category, int> {
        { Category.Hate, 4 }, { Category.SelfHarm, 4 }, { Category.Sexual, 4 }, { Category.Violence, 4 }
    };

    // Make a decision based on the detection result and reject thresholds
    Decision decisionResult = contentSafety.MakeDecision(detectionResult, rejectThresholds);
}
