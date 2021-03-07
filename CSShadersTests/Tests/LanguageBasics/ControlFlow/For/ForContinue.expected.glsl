#version 450

struct ForContinue
{
    int empty_struct_member;
};

void ForContinue_PreConstructor(ForContinue self)
{
}

void ForContinue_DefaultConstructor(ForContinue self)
{
    ForContinue_PreConstructor(self);
}

int ForContinue0(ForContinue self, int count)
{
    int result = 0;
    for (int i = 1; i < count; i++)
    {
    }
    return result;
}

int ForContinue1(ForContinue self, int count)
{
    int result = 0;
    for (int i = 1; i < count; i++)
    {
        result++;
    }
    return result;
}

void Main(ForContinue self)
{
    int ret = 0;
    ret = ForContinue0(self, 10);
    ret = ForContinue1(self, 10);
}

void main()
{
    ForContinue self;
    ForContinue_DefaultConstructor(self);
    Main(self);
}

