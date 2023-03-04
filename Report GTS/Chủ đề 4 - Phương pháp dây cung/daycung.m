    tic
    %%%%%% input %%%%%%
    syms x;
    %f = x^3+2*x-11;
    %f = log(x)-1 
    f= tan(x/4)-1
    a = 2
    b = 4
    err = 0.0001
    %%%%%%%%%%%%%%%%%%%
    g= diff(f, x)
    h= diff(g, x)
    if subs(f,a)*subs(f,b)>0
        t=0
    elseif subs(f,a)==0
        t=0
        fprintf('Nghiem la x=a=%f', a)
        return
    elseif subs(f,b)==0
        t=0
        fprintf("Nghiem la x=b=%f", b)
        return
    else
        u=-g
        g=matlabFunction(g)
        u=matlabFunction(u)
        [xmin, ymin]=fminbnd(g, a, b)
        m1=ymin
        [xmax, ymax]=fminbnd(u, a, b)
        M1=-ymax
        if m1*M1 <= 0
            t=0
        else
            v=-h
            h=matlabFunction(h)
            v=matlabFunction(v)
            [x1min, y1min]=fminbnd(h, a, b)
            m2=y1min
            [x1max, y1max]=fminbnd(v, a, b)
            M2=-y1max
            if m2*M2 <= 0
                t=0
            else
                t = 1
            end
        end
    end
    if t == 0
        fprintf("Khong thoa man dieu kien dau vao\n")
    else
       z =sign(feval(h,a))
       m1= abs(m1)
       if subs(f,a)*z < 0
            x0 = a;
            d = b;
       else
            x0 = b;
            d = a;
       end
    iter = 1;
    x1 = x0 - subs(f,x0)*(d - x0)/(subs(f,d)-subs(f,x0))
    while abs(subs(f,x1))/m1 >= err
        x0=x1
        x1 = x0 - subs(f,x0)*(d - x0)/(subs(f,d)-subs(f,x0))
        iter = iter + 1;
    end

    fprintf("Nghiem cua phuong trinh la %f\n", double(x1));
    fprintf("Sai so: %f\n", double(abs(subs(f,x1)))/m1);
    end
    toc