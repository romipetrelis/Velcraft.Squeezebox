namespace Velcraft.Squeezebox.Tests.Steps
{
	public class PlayerStepsScenarioContext
	{
		public PlayerStepsScenarioContext()
		{
			ShuffleHistory = new object[3];
			RepeatHistory = new object[3];
		}

		public ISqueezeboxService Service {get;set;}
		public string PlayerId {get;set;}
		public string BeforeTrackId {get;set;}
		public string AfterTrackId {get;set;}
		public int ShuffleIndex {get;set;}
		public object[] ShuffleHistory { get; private set; }
		public int RepeatIndex {get;set;}
		public object[] RepeatHistory { get; private set; }
	}
}

