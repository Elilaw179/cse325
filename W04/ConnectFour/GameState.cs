namespace ConnectFour;

public class GameState
{
    public enum WinState
    {
        No_Winner,
        Player1_Wins,
        Player2_Wins,
        Tie
    }

    private readonly byte[] board = new byte[42];

    public byte PlayerTurn { get; private set; } = 1;

    public int CurrentTurn { get; private set; }

    // Game history
    public int GamesPlayed { get; private set; }

    public int Player1Wins { get; private set; }

    public int Player2Wins { get; private set; }

    public int Ties { get; private set; }

    private bool gameResultRecorded = false;

    public void ResetBoard()
    {
        Array.Clear(board, 0, board.Length);

        PlayerTurn = 1;
        CurrentTurn = 0;

        gameResultRecorded = false;
    }

    public bool IsColumnFull(byte col)
    {
        if (col > 6)
        {
            return true;
        }

        return board[col] != 0;
    }

    public byte PlayPiece(byte col)
    {
        if (col > 6)
        {
            throw new ArgumentException("Invalid column.");
        }

        if (IsColumnFull(col))
        {
            throw new ArgumentException("That column is full.");
        }

        for (byte row = 5; row >= 0; row--)
        {
            int position = row * 7 + col;

            if (board[position] == 0)
            {
                board[position] = PlayerTurn;

                CurrentTurn = position;

                byte landingRow = (byte)(row + 1);

                PlayerTurn = PlayerTurn == 1
                    ? (byte)2
                    : (byte)1;

                return landingRow;
            }
        }

        throw new ArgumentException("That column is full.");
    }

    public WinState CheckForWin()
    {
        // Horizontal
        for (int row = 0; row < 6; row++)
        {
            for (int col = 0; col < 4; col++)
            {
                int index = row * 7 + col;

                if (board[index] != 0 &&
                    board[index] == board[index + 1] &&
                    board[index] == board[index + 2] &&
                    board[index] == board[index + 3])
                {
                    return board[index] == 1
                        ? WinState.Player1_Wins
                        : WinState.Player2_Wins;
                }
            }
        }

        // Vertical
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 7; col++)
            {
                int index = row * 7 + col;

                if (board[index] != 0 &&
                    board[index] == board[index + 7] &&
                    board[index] == board[index + 14] &&
                    board[index] == board[index + 21])
                {
                    return board[index] == 1
                        ? WinState.Player1_Wins
                        : WinState.Player2_Wins;
                }
            }
        }

        // Diagonal down-right
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 4; col++)
            {
                int index = row * 7 + col;

                if (board[index] != 0 &&
                    board[index] == board[index + 8] &&
                    board[index] == board[index + 16] &&
                    board[index] == board[index + 24])
                {
                    return board[index] == 1
                        ? WinState.Player1_Wins
                        : WinState.Player2_Wins;
                }
            }
        }

        // Diagonal down-left
        for (int row = 0; row < 3; row++)
        {
            for (int col = 3; col < 7; col++)
            {
                int index = row * 7 + col;

                if (board[index] != 0 &&
                    board[index] == board[index + 6] &&
                    board[index] == board[index + 12] &&
                    board[index] == board[index + 18])
                {
                    return board[index] == 1
                        ? WinState.Player1_Wins
                        : WinState.Player2_Wins;
                }
            }
        }

        // Tie
        if (board.All(piece => piece != 0))
        {
            return WinState.Tie;
        }

        return WinState.No_Winner;
    }

    public void RecordResult(WinState result)
    {
        if (gameResultRecorded)
        {
            return;
        }

        if (result == WinState.No_Winner)
        {
            return;
        }

        GamesPlayed++;

        switch (result)
        {
            case WinState.Player1_Wins:
                Player1Wins++;
                break;

            case WinState.Player2_Wins:
                Player2Wins++;
                break;

            case WinState.Tie:
                Ties++;
                break;
        }

        gameResultRecorded = true;
    }

    public void ClearHistory()
    {
        GamesPlayed = 0;
        Player1Wins = 0;
        Player2Wins = 0;
        Ties = 0;

        gameResultRecorded = false;
    }
}