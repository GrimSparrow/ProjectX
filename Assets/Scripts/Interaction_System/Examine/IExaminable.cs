public interface IExaminable
{
    ExaminableBase  Prepare();
    void Drag(float speed);

    void Use();//возможно тут стоит вызывать коллбэк
}
