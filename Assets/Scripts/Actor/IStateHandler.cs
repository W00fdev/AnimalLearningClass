namespace StudyProject
{
    public enum StateActive { PASSIVE, SPEAKING, FLAPPING, TABLE, FIGURE }
    public enum StatePassive { DEFAULT, HAPPY, SAD, INCOMPREHESION, SPEAKING, PROUD, ANGRY, SURPRISED, SMILING }

    // Animal_st         ==    DEFAULT            = 0
    // Animal_fun        ==    HAPPY              = 1
    // Animal_sad        ==    SAD                = 2
    // Animal_underst    ==    INCOMPREHESION     = 3
    // Animal_sp         ==    SPEAKING           = 4
    // Animal_gor        ==    PROUD              = 5
    // Animal_ang        ==    ANGRY              = 6
    // Animal_underst    ==    SURPRISED          = 7
    // Animal_st         ==    SMILING            = 8

    public interface IStateHandler
    {
    }
}
