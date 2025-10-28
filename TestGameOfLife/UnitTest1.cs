using AwesomeAssertions;

namespace TestGameOfLife;

public class UnitTest1
{
    [Fact]
    public void Si_NoEstaAgregadaUnaCelulaEnLaPosicionDada_Debe_EstarMuertaEstaPosicion()
    {
        
        //Arrange
        var gameOfLife = new GameOfLife(new bool[5,5]);
        //Act
        var estaCelulaViva = gameOfLife.SaberSiCelulaEstaViva(1, 1);
        //Assert
        estaCelulaViva.Should().Be(false);
    }

    [Theory]
    [InlineData(1,1)]
    [InlineData(1,2)]
    public void Si_AgregoUnaCelulaEnLaPosicionDada_Debe_EstarViva(int ubicacionX, int ubicacionY)
    {
        //Arrange
        var gameOfLife = new GameOfLife(new bool[5, 5]);
        gameOfLife.AgregarCelula(ubicacionX,ubicacionY);
        //Act
        var estaCelulaViva = gameOfLife.SaberSiCelulaEstaViva(ubicacionX, ubicacionY);
        //
        estaCelulaViva.Should().Be(true);
    }
    
    [Theory]
    [InlineData(1,1,2,2)]
    [InlineData(1,2,3,3)]
    [InlineData(3,3,1,1)]
    public void Si_SeAgregadaUnaUnicaCelulaEnLaPosicionDada_Debe_EstarMuertaLasCelulasEnOtrasPociciones(int ubicacionX, int ubicacionY,int ubicacionMuertaX, int ubicacionMuertaY)
    {
        
        //Arrange
        var gameOfLife = new GameOfLife(new bool[5,5]);
        gameOfLife.AgregarCelula(ubicacionX,ubicacionY);
        //Act
        var estaCelulaViva = gameOfLife.SaberSiCelulaEstaViva(ubicacionMuertaX, ubicacionMuertaY);
        //Assert
        estaCelulaViva.Should().Be(false);
    }
    
    //Cualquier célula viva con menos de dos vecinas vivas muere, como si la causa fuera la infrapoblación.
    [Theory]
    [InlineData(3,3)]
    [InlineData(2,1)]
    public void Si_UnaCelulaNoTieneVecinasVivas_Debe_MorirEnLaSiguienteGeneracion(int ubicacionX, int ubicacionY)
    {
        //Arrange
        var gameOfLife = new GameOfLife(new bool[5,5]);
        gameOfLife.AgregarCelula(ubicacionX,ubicacionY);
        gameOfLife.NextGen();
        
        //Act
        var estaCelulaViva = gameOfLife.SaberSiCelulaEstaViva(ubicacionX, ubicacionY);
        //Assert
        estaCelulaViva.Should().Be(false);
    }
    
    //Cualquier célula viva con dos o tres vecinas vivas pasa a la siguiente generación.
    [Theory]
    [InlineData(3,3,2,3,4,3)]
    [InlineData(2,2,2,3,2,1)]
    [InlineData(2,2,3,3,1,1)]
    public void Si_UnaCelulaCon2VecinasVivas_Debe_VivirEnLaSiguienteGeneracion(int ubicacionX, int ubicacionY, int vecino1X , int vecino1Y, int vecino2X, int vecino2Y)
    {
        //Arrange
        var gameOfLife = new GameOfLife(new bool[5,5]);
        gameOfLife.AgregarCelula(ubicacionX,ubicacionY);
        gameOfLife.AgregarCelula(vecino1X,vecino1Y);
        gameOfLife.AgregarCelula(vecino2X ,vecino2Y);
        gameOfLife.NextGen();
        
        //Act
        var estaCelulaViva = gameOfLife.SaberSiCelulaEstaViva(ubicacionX, ubicacionY);
        //Assert
        estaCelulaViva.Should().Be(true);
    }
    
}

public class GameOfLife
{
    public bool[,] boardGameOfLife;
    public bool[,] boardGameOfLifeSiguienteGeneracion;
    public GameOfLife(bool[,] board)
    {
        boardGameOfLife = board;
        boardGameOfLifeSiguienteGeneracion = (bool[,])board.Clone();
    }

    public void NextGen()
    {
        
        for (int y = 0; y < boardGameOfLife.GetLength(1); y++)
        {
            for (int x = 0; x < boardGameOfLife.GetLength(0); x++)
            {   
                if (x == 0 || x == ObtenerLimiteMaximoX() || y == 0 || y == ObtenerLimiteMaximoY())
                {
                    boardGameOfLifeSiguienteGeneracion[x,y] = false;
                    continue;
                }

                if (boardGameOfLife[x + 1, y] || boardGameOfLife[x - 1, y] || boardGameOfLife[x, y + 1] || boardGameOfLife[x, y - 1] || boardGameOfLife[x - 1, y - 1] || boardGameOfLife[x + 1, y + 1] || boardGameOfLife[x + 1, y - 1])
                {
                    boardGameOfLifeSiguienteGeneracion[x,y] = true;
                }
                else
                {
                    boardGameOfLifeSiguienteGeneracion[x,y] = false;    
                }
            }
        }
        boardGameOfLife = boardGameOfLifeSiguienteGeneracion;
    }

    public object SaberSiCelulaEstaViva(int ubicacionX, int ubicacionY)
    { 
        return boardGameOfLife[ubicacionX,ubicacionY];
    }

    public void AgregarCelula(int ubicacionX, int ubicacionY)
    { 
        boardGameOfLife[ubicacionX, ubicacionY] = true;
    }

    public int ObtenerLimiteMaximoX()
    {
        return boardGameOfLife.GetLength(0) - 1;
    }
    public int ObtenerLimiteMaximoY()
    {
        return boardGameOfLife.GetLength(1) - 1;
    }


    
}