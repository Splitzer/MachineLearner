namespace MachineLearner.Algorithms
{
    public class LearningManager
    {
        int N, d;
        int maxValue, minValue, range;
        float[] f;
        float[][] n;
        float[][] p, p2;

        public LearningManager()
        {
            InitialiseDataset();
        }

        private static void InitialiseDataset()
        {
            //int N = size(X,1);
            //int d = size(X,2);
            //int megtimi = max(max(X));
            float[][] f;

            //for (int i = 0; i < megtimi; i++)
            //{
                //    for k=1:d
                //        f(i,k)=sum(X(:,k) == i);
                //    end
            //}

            //for (int i = 0; i < d; i++)
            //{
                //    n(k)=size(find(f(:,k)~=0),1);
            //}

            //float p = f / N;
            //float p2 = (f.*(f-1))./(N*(N-1));

            int range=0;
            int Xmin=0;
            //func_inputs={n f  pp2 N range Xmin};
            //[range,Xmin] = Normalise(X,func_names,func_inputs); %
            //func_inputs={n f p p2 N range Xmin};
        }

        private void Normalise()
        {
            //function [range,Xmin] = DistNorm(X,func_names,func_inputs)

            //N=func_inputs{1,5};
            //shroom1=X(1,:);

            //for i=1:N
            //    for F=1:14
            //        Dist(i,F)=feval(func_names{F,1},shroom1,X(i,:),func_inputs);
            //    end
            //end

            //Xmax=max(Dist);
            //% Xmax=Dist(end,:);

            //for F=1:14
            //    tab=tabulate(Dist(:,F));
            //    top=tab(:,1);
    
            //    if(top(end)==1e4)
            //        Xmax(F)=top(end-1)*10;
            //    else
            //        Xmax(F)=top(end);
            //    end

            //end

            //Xmin=min(Dist);
            //% Xmin=Dist(1,:);

            //range=Xmax-Xmin;

            //end
        }

        public float[] LinearRegression(float[][] X, float[] Y)
        {
            float[] W;

            W = Y;

            return W;
        }

        private float CalculateDistance(float x, float y)
        {
            return 0;
        }

        private float[] Prediction(float[] Y, float[][] W, float[][] Z)
        {
            float[] Ypred;

            Ypred = Y;

            return Ypred;
        }

        private void RBFCompute()
        {
            //function [W,Zval,L] = RBFcompute(Xtrain,Xval,Y,distfactor,func_names,func_inputs,F)

            //Ntrain=size(Xtrain,1);
            //Nval=size(Xval,1);
            //kernels=Xtrain(1,:);

            //%% RBFs compute

            //for i=2:Ntrain
            //    for l=1:size(kernels,1)
            //        D(l)=feval(func_names{F,1},Xtrain(i,:),kernels(l,:),func_inputs);
            //    end
            //    if min(D) > distfactor
            //       kernels=cat(1,kernels,Xtrain(i,:));
            //    end
            //end

            //% for i=2:N
            //%     l=1;
            //%     D=0;
            //%     
            //%      while l <= size(kernels,1) && D < distfactor
            //%         D=feval(func_names{F,1},Xtrain(i,:),kernels(l,:),func_inputs);
            //%         l=l+1;
            //%      end
            //%      if D >= distfactor
            //%             kernels = cat(1,kernels,Xtrain(i,:));
            //%             break;
            //%      end
            //% end

            //%% Training Z and W compute

            //L=size(kernels,1);
            //Z=zeros(Ntrain,L);
            //func_inputs{1,6}=0;

            //for i=1:Ntrain
            //    for l=1:L
            //        Z(i,l)=feval(func_names{F,1},Xtrain(i,:),kernels(l,:),func_inputs);
            //    end    
            //end

            //Z=(Z.^2).*log10(Z+1);
            //Z=[Z ones(Ntrain,1)];
            //W=((Z'*Z)^-1)*Z'*Y;

            //%% Validation Z compute

            //Zval=zeros(Nval,L);

            //for i=1:Nval
            //    for l=1:L
            //        Zval(i,l)=feval(func_names{F,1},Xval(i,:),kernels(l,:),func_inputs);
            //    end
            //end

            //Zval=(Zval.^2).*log10(Zval+1);
            //Zval=[Zval ones(Nval,1)];

            //end
        }

        //       for distfactor=index
//           iter=iter+1
//           for F=Findex
//               [W,Zval,L(iter,F)] = RBFcompute(Xtrain,Xval,Ytrainconv,distfactor,func_names,func_inputs,F);
//               [Accval(iter,F)] = Prediction(Yvalconv,W,Zval)        
//       %         ErrorVal(iter,:) = PredictionIndiv(Yvalconv,W,Zval)
//               iter
//           end
//       end

//       %%
//       Acctest=zeros(size(Findex,2),1);
//       Ltst=zeros(size(Findex,2),1);
//       for F=Findex
//           optimaldist(F) = OptDist(Accval(:,F),index);
//           [W,Ztest,Ltst(F)] = RBFcompute(Xtrain,Xtest,Ytrainconv,optimaldist,func_names,func_inputs,F);
//           [Acctest(F)] = Prediction(Ytestconv,W,Ztest)
//       %     ErrorTest = PredictionIndiv(Ytestconv,W,Ztest)
//       end
    }
}