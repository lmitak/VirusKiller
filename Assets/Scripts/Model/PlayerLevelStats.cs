using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class PlayerLevelStats {

	public int levelNumber { get; set; }
    public int highscore { get; set; }

    public PlayerLevelStats(int level, int highscore)
    {
        this.levelNumber = level;
        this.highscore = highscore;
    }

    public PlayerLevelStats()
    { }
}
