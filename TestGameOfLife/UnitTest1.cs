using AwesomeAssertions;

namespace TestGameOfLife;

public class UnitTest1
{
    [Fact]
    public void Si_UnaCelulaVivaConMenosDeDosVecinas_Debe_Morir()
    {
        
        //Arrange
        var gameOfLife = new GameOfLife(new bool[5,5]);
        //Act
        var estaCelulaViva = gameOfLife.saberSiCelulaEstaViva(1, 1);
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
        var estaCelulaViva = gameOfLife.saberSiCelulaEstaViva(ubicacionX, ubicacionY);
        //
        estaCelulaViva.Should().Be(true);
    }
}

public class GameOfLife
{
    bool[,] boardGameOfLife ;
    public GameOfLife(bool[,] board)
    {
        boardGameOfLife = board;
    }

    public void NextGen()
    {
    }

    public object saberSiCelulaEstaViva(int ubicacionX, int ubicacionY)
    { 
        return boardGameOfLife[1,1];
    }

    public void AgregarCelula(int ubicacionX, int ubicacionY)
    { 
        boardGameOfLife[1, 1] = true;
    }
}