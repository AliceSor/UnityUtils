Saving package : v1.0


How use:
0) Include "using SavingSystem;"
1) Create data class to hold all data that must be saved - note - it must be class and not struct;
2) Create c# script that extend SaverSO<T> and where T is your data class, for example, like this: "public class CustomExampleSaver : SaverSO<PersistentData>" and add [CreateAssetMenu()] attribute;
3) Create ScriptableObject (SO) from your script. Reference this SO from your monobehaviors to get acsses to your saved data.
