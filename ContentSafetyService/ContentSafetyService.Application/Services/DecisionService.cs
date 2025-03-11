using ContentSafetyService.Domain;
using ContentSafetyService.Domain.Enums;
using Action = ContentSafetyService.Domain.Enums.Action;

namespace ContentSafetyService.Application.Services;

public class DecisionService : IDecisionService
{
    /// <summary>
    /// The valid threshold values.
    /// </summary>
    public static readonly int[] VALID_THRESHOLD_VALUES = { -1, 0, 2, 4, 6 };

    Decision IDecisionService.MakeDecision(DetectionResult detectionResult, Dictionary<Category, int> rejectThresholds)
    {
        Dictionary<Category, Action> actionResult = new Dictionary<Category, Action>();
        Action finalAction = Action.Accept;
        foreach (KeyValuePair<Category, int> pair in rejectThresholds)
        {
            if (!VALID_THRESHOLD_VALUES.Contains(pair.Value))
            {
                throw new ArgumentException("RejectThreshold can only be in (-1, 0, 2, 4, 6)");
            }

            int? severity = GetDetectionResultByCategory(pair.Key, detectionResult);
            if (severity == null)
            {
                throw new ArgumentException($"Can not find detection result for {pair.Key}");
            }

            Action action;
            if (pair.Value != -1 && severity >= pair.Value)
            {
                action = Action.Reject;
            }
            else
            {
                action = Action.Accept;
            }
            actionResult[pair.Key] = action;

            if (action.CompareTo(finalAction) > 0)
            {
                finalAction = action;
            }
        }

        // blocklists
        if (detectionResult is TextDetectionResult textDetectionResult)
        {
            if (textDetectionResult.BlocklistsMatch != null &&
                textDetectionResult.BlocklistsMatch.Count > 0)
            {
                finalAction = Action.Reject;
            }
        }

        Console.WriteLine(finalAction);
        foreach (var res in actionResult)
        {
            Console.WriteLine($"Category: {res.Key}, Action: {res.Value}");
        }

        return new Decision(finalAction, actionResult);
    }

    int? GetDetectionResultByCategory(Category category, DetectionResult detectionResult)
    {
        int? severityResult = null;
        if (detectionResult.CategoriesAnalysis != null)
        {
            foreach (var detailedResult in detectionResult.CategoriesAnalysis)
            {
                if (detailedResult.Category == category.ToString())
                {
                    severityResult = detailedResult.Severity;
                }
            }
        }

        return severityResult;
    }
}